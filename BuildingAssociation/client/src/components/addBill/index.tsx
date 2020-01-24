import * as React from 'react';
import './style.css';
import { RouteComponentProps, Redirect } from 'react-router';
import { Provider } from '../../models/Provider';
import { ProviderBill } from '../../models/ProviderBill';

import moment from 'moment';
import ReactDatePicker from 'react-datepicker';
import { Mansion } from '../../models/Mansion';

interface AddBillState {
    providers: Provider[];
    selectedProvider?: Provider;
    mansions: Mansion[];
    selectedMansion?: Mansion;
    units: number;
    other: number;
    dueDate: string;
    saved: boolean;
    date: string;
}

export default class AddBill extends React.Component<RouteComponentProps<any>, AddBillState> {
    constructor(props: RouteComponentProps<any>) {
        super(props);

        this.state = {
            providers: [],
            selectedProvider: undefined,
            mansions: [],
            selectedMansion: undefined,
            units: 0,
            other: 0,
            dueDate: moment.utc().startOf('day').toISOString(),
            saved: false,
            date: moment.utc().startOf('day').toISOString()
        }
    }

    componentDidMount() {
        this.initData();
    }

    initData = async () => {
        if(sessionStorage.getItem('authToken') != null) {
            const providersFromDb = await fetch(`/providers`, {
                headers: {
                    'Authorization': sessionStorage.getItem('authToken')
                } 
            } as RequestInit).then(response => {
                if(response.ok) {
                return response.json();
                }
    
                return undefined;
            });

            const mansionsFromDb = await fetch(`/mansions`, {
                headers: {
                    'Authorization': sessionStorage.getItem('authToken')
                } 
            } as RequestInit).then(response => {
                if(response.ok) {
                return response.json();
                }
    
                return undefined;
            });

            this.setState({providers: providersFromDb, mansions: mansionsFromDb});
        }
    }
 
    calculatePrice = () => {
        const unitPrice = this.state.selectedProvider?.unitPrice as number;
        return unitPrice * this.state.units + this.state.other;
    }

    renderProviders = () => {
        return this.state.providers && this.state.providers.map((provider: Provider) => {
            return <option value={provider.providerId}>{provider.name}</option>
        })
    }

    renderMansions = () => {
        return this.state.mansions && this.state.mansions.map((mansion: Mansion) => {
            return <option value={mansion.id}>{mansion.address}</option>
        })
    }


    selectProvider = (e: React.ChangeEvent<HTMLSelectElement>) => {
        const selectedOptionId = parseInt(e.target.selectedOptions[0].value);
        const selectedPv = this.state.providers.find(x => x.providerId === selectedOptionId);

        if(selectedPv) {
            this.setState({selectedProvider: selectedPv});
        }
    }

    selectMansion = (e: React.ChangeEvent<HTMLSelectElement>) => {
        const selectedOptionId = parseInt(e.target.selectedOptions[0].value);
        const selectedM = this.state.mansions.find(x => x.id === selectedOptionId);

        if(selectedM) {
            this.setState({selectedMansion: selectedM});
        }
    }

    submit = (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();

        const providerBill: ProviderBill = {
            dueDate: this.state.dueDate,
            date: this.state.date,
            other: this.state.other,
            units: this.state.units,
            providerId: this.state.selectedProvider?.providerId,
            paid: false,
            providerUnitPrice: this.state.selectedProvider?.unitPrice,
            providerName: this.state.selectedProvider?.name as string,
            mansionId: this.state.selectedMansion?.id,
            mansionName: this.state.selectedMansion?.address as string,
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

    setDate = (date: Date) => {
        if(date) {
            this.setState({date: moment.utc(date).startOf('day').toISOString()});
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
                <h3>Add bill</h3>
                <label>Mansion</label>
                <select required className="form-control" onChange={this.selectMansion}>
                    <option value="">---</option>
                    {this.renderMansions()}
                </select>
                <label>Provider</label>
                <select required className="form-control" onChange={this.selectProvider}>
                    <option value="">---</option>
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
                    <label>Date</label>
                    <br/>
                    <ReactDatePicker 
                    className="form-control"
                        onChange={this.setDate}
                        selected={moment(this.state.date).toDate()}
                    />
                </div>
                <div className="form-group">
                    <label>Due date</label>
                    <br/>
                    <ReactDatePicker 
                    className="form-control"
                        onChange={this.setDueDate}
                        minDate={moment(this.state.date).toDate()}
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