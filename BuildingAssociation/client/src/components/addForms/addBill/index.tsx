import * as React from 'react';
import './style.css';
import { RouteComponentProps, Redirect } from 'react-router';
import moment from 'moment';
import ReactDatePicker from 'react-datepicker';
import { Provider } from '../../../models/Provider';
import { Mansion } from '../../../models/Mansion';
import { ProviderBill } from '../../../models/ProviderBill';

interface AddBillState {
    providers: Provider[];
    selectedProvider?: Provider;
    mansions: Mansion[];
    selectedMansion?: Mansion;
    units: number | undefined;
    other: number | undefined;
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
            units: undefined,
            other: 0,
            dueDate: moment.utc().startOf('day').toISOString(),
            saved: false,
            date: moment.utc().startOf('day').toISOString(),
        }
    }

    componentDidMount() {
        const { id } = this.props.match.params;
        this.initData(id);
    }

    initData = async (id?: number) => {
        if(sessionStorage.getItem('authToken') != null) {
            const providersFromDb: Provider[] = await fetch(`/providers`, {
                headers: {
                    'Authorization': sessionStorage.getItem('authToken')
                } 
            } as RequestInit).then(response => {
                if(response.ok) {
                return response.json();
                }
    
                return undefined;
            });

            const mansionsFromDb: Mansion[] = await fetch(`/mansions`, {
                headers: {
                    'Authorization': sessionStorage.getItem('authToken')
                } 
            } as RequestInit).then(response => {
                if(response.ok) {
                return response.json();
                }
    
                return undefined;
            });


            if(id) {
                const item: ProviderBill = await fetch(`/providerbills/${id}`, {
                    headers: {
                        'Authorization': sessionStorage.getItem('authToken')
                    } 
                } as RequestInit).then(response => {
                    if(response.ok) {
                    return response.json();
                    }
        
                    return undefined;
                });

                if(item) {
                    this.setState({
                        providers: providersFromDb,
                        mansions: mansionsFromDb,
                        date: item.date,
                        dueDate: item.dueDate,
                        other: item.other,
                        units: item.units,
                        selectedMansion: mansionsFromDb.find(x => x.id === item.mansionId),
                        selectedProvider: providersFromDb.find(x => x.providerId === item.providerId)
                    })
                }
            } else {
                this.setState({
                    providers: providersFromDb,
                    mansions: mansionsFromDb,
                });
            }
        }
    }
 
    calculatePrice = () => {
        const unitPrice = this.state.selectedProvider?.unitPrice as number;
        return this.state.units ? (unitPrice * this.state.units + (this.state.other ?  this.state.other : 0)) : "";
    }

    renderProviders = () => {
        return this.state.providers && this.state.providers.map((provider: Provider, index: number) => {
            const isSelected = this.state.selectedProvider && this.state.selectedProvider.providerId === provider.providerId;
            return <option key={`${provider.providerId}-${index}`} value={provider.providerId} selected={isSelected}>{provider.name}</option>
        })
    }

    renderMansions = () => {
        return this.state.mansions && this.state.mansions.map((mansion: Mansion, index: number) => {
            const isSelected = this.state.selectedMansion && this.state.selectedMansion.id === mansion.id;
            return <option key={`${mansion.id}-${index}`} value={mansion.id} selected={isSelected}>{mansion.address}</option>
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
            other: this.state.other as number,
            units: this.state.units as number,
            providerId: this.state.selectedProvider?.providerId,
            paid: false,
            providerUnitPrice: this.state.selectedProvider?.unitPrice,
            providerName: this.state.selectedProvider?.name as string,
            mansionId: this.state.selectedMansion?.id,
            mansionName: this.state.selectedMansion?.address as string,
            billId: this.props.match.params.id,
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
            this.setState({dueDate: moment(date).startOf('day').toISOString()});
        }
    }

    setDate = (date: Date) => {
        if(date) {
            this.setState({date: moment(date).startOf('day').toISOString()});
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
           return <Redirect to="/billlist" /> 
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
                        defaultValue={this.state.units}
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
                        defaultValue={this.state.other}
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