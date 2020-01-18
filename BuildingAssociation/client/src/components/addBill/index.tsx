import * as React from 'react';
import './style.css';
import { RouteComponentProps, Redirect } from 'react-router';
import { Provider } from '../../models/provider';
import { ProviderBillViewModel } from '../../models/providerBill';

import moment from 'moment';
import ReactDatePicker from 'react-datepicker';

interface AddBillState {
    providers: Provider[];
    selectedProvider?: Provider;
    units: number;
    other: number;
    dueDate: string;
    loading: boolean;
    saved: boolean;
}

export default class AddBill extends React.Component<RouteComponentProps<any>, AddBillState> {
    constructor(props: RouteComponentProps<any>) {
        super(props);

        this.state = {
            providers: [],
            selectedProvider: undefined,
            units: 0,
            other: 0,
            dueDate: moment.utc().startOf('day').toISOString(),
            loading: true,
            saved: false
        }
    }

    componentDidMount() {
        this.initData();
    }

    initData = () => {
        if(sessionStorage.getItem('authToken') != null) {
            fetch(`/providers`, {
                headers: {
                    'Authorization': sessionStorage.getItem('authToken')
                } 
            } as RequestInit).then(response => {
                if(response.ok) {
                return response.json();
                }

                return undefined;
            }).then(result => this.setState({providers: result }));
        }
    }

    renderProviders = () => {
        return this.state.providers && this.state.providers.map((provider: Provider) => {
            return <option value={provider.uniqueId}>{provider.name}</option>
        })
    }

    calculatePrice = () => {
        const unitPrice = this.state.selectedProvider?.unitPrice as number;
        return unitPrice * this.state.units + this.state.other;
    }

    selectProvider = (e: React.ChangeEvent<HTMLSelectElement>) => {
        const selectedOptionId = parseInt(e.target.selectedOptions[0].value);
        const selectedPv = this.state.providers.find(x => x.uniqueId === selectedOptionId);

        if(selectedPv) {
            this.setState({selectedProvider: selectedPv});
        }
    }

    submit = (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();

        const providerBill: ProviderBillViewModel = {
            dueDate: this.state.dueDate,
            other: this.state.other,
            units: this.state.units,
            providerId: this.state.selectedProvider?.uniqueId,
            paid: false,
            providerUnitPrice: this.state.selectedProvider?.unitPrice
        }

        fetch('/providerBills', {
            method: 'POST',
            body: JSON.stringify(providerBill),
            headers: {
                'Content-Type': 'application/json',
                'Authorization': sessionStorage.getItem('authToken')
            }
        } as RequestInit).then(result => { this.setState({saved: true})});
    }

    setDueDate = (date: Date) => {
        if(date) {
            this.setState({dueDate: moment.utc(date).startOf('day').toISOString()});
        }
    }

    render() {
        if(sessionStorage.getItem('authToken') == null) {
            return <div>
                Nu esti logat!
            </div>
        }

        if(this.state.saved)
        {
           return <Redirect to="/" /> 
        }
        return (
            <form className="container addbill-container" onSubmit={this.submit}>
                <select required className="form-control" onChange={this.selectProvider}>
                    <option>---</option>
                    {this.renderProviders()}
                </select>
                <div className="form-group">
                    <label>Units</label>
                    <input
                        type="number"
                        step="any"
                        onChange={(e) => this.setState({units: parseFloat(e.target.value)}) }
                        min="0"
                        className="form-control"
                        required
                    />
                </div>
                <div className="form-group">
                    <label>Other</label>
                    <input 
                        type="number"
                        step="any"
                        onChange={(e) => this.setState({other: parseFloat(e.target.value)}) }
                        min="0"
                        className="form-control"
                        required
                    />
                </div>
                <div className="form-group">
                    <label>Due date</label>
                    <br/>
                    <ReactDatePicker 
                    className="form-control"
                        onChange={this.setDueDate}
                        minDate={moment.utc().startOf('day').toDate()}
                        selected={moment(this.state.dueDate).toDate()}
                    />
                </div>
                {this.state.selectedProvider && <div><b>Total price: {this.calculatePrice()}</b></div>}
                <br/>
                <button type="submit" className="btn btn-primary">Submit</button>
            </form>
        )
    }
}