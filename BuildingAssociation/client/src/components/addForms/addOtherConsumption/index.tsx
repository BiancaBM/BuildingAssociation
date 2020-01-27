import * as React from 'react';
import { RouteComponentProps, Redirect } from 'react-router';
import moment from 'moment';
import ReactDatePicker from 'react-datepicker';
import { Mansion } from '../../../models/Mansion';
import { OtherConsumption } from '../../../models/OtherConsumption';
import { CalculationType } from '../../../models/CalculationType';

interface AddOtherConsumptionState {
    name: string;
    calculationType?: CalculationType;
    date: string;
    price?: number;
    saved: boolean;
    mansions: Mansion[];
    selectedMansion?: Mansion;
}

export default class AddOtherConsumption extends React.Component<RouteComponentProps<any>, AddOtherConsumptionState> {
    constructor(props: RouteComponentProps<any>) {
        super(props);

        this.state = {
            name: "",
            calculationType: undefined,
            date: moment.utc().startOf('day').toISOString(),
            price: undefined,
            saved: false,
            mansions: [],
            selectedMansion: undefined
        }
    }

    componentDidMount() {
            const { id } = this.props.match.params;
            this.initData(id);
    }

    initData = async (id?: number) => {
        const mansionsFromDb : Mansion[] = await fetch(`/mansions`, {
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
            const item: OtherConsumption = await fetch(`/otherconsumptions/${id}`, {
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
                    mansions: mansionsFromDb,
                    calculationType: item.calculationType,
                    date: item.date,
                    name: item.name,
                    price: item.price,
                    selectedMansion: mansionsFromDb.find(x => x.id === item.mansionId)
                })
            }
        } else {
            this.setState({mansions: mansionsFromDb});
        }
    }

    submit = (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();

        const item: OtherConsumption = {
            name: this.state.name,
            calculationType: this.state.calculationType as CalculationType,
            date: this.state.date,
            price: this.state.price as number,
            mansionId: this.state.selectedMansion?.id,
            mansionName: this.state.selectedMansion?.address as string,
            id: this.props.match.params.id
        }

        fetch('/otherconsumptions', {
            method: 'POST',
            body: JSON.stringify(item),
            headers: {
                'Content-Type': 'application/json',
                'Authorization': sessionStorage.getItem('authToken')
            }
        } as RequestInit).then(result => { this.setState({saved: true})});
    }

    renderMansions = () => {
        return this.state.mansions && this.state.mansions.map((mansion: Mansion, index: number) => {
            return <option key={`${mansion.id}-${index}`} value={mansion.id}>{mansion.address}</option>
        })
    }

    selectMansion = (e: React.ChangeEvent<HTMLSelectElement>) => {
        const selectedOptionId = parseInt(e.target.selectedOptions[0].value);
        const selectedM = this.state.mansions.find(x => x.id === selectedOptionId);

        if(selectedM) {
            this.setState({selectedMansion: selectedM});
        }
    }

    selectType = (e: React.ChangeEvent<HTMLSelectElement>) => {
        const selectedOptionId = parseInt(e.target.selectedOptions[0].value);
        this.setState({calculationType: selectedOptionId});
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
           return <Redirect to="/consumptions" /> 
        }

        return (
            <form className="container addconsumption-container" onSubmit={this.submit}>
                <h3>Add consumption</h3>
                <div className="form-group">
                    <label>Name</label>
                    <input
                        type="text"
                        onChange={(e) => this.setState({name: e.target.value}) }
                        className="form-control"
                        required
                        defaultValue={this.state.name}
                    />
                </div>
                <label>Mansion</label>
                <select required className="form-control" onChange={this.selectMansion} value={this.state.selectedMansion?.id}>
                    <option value="">---</option>
                    {this.renderMansions()}
                </select>
                <div className="form-group">
                    <label>Calculation type</label>
                    <select className="form-control" onChange={this.selectType} required defaultValue={this.state.calculationType}>
                        <option value="">---</option>
                        <option value={CalculationType.IndividualQuota}>Individual quota</option>
                        <option value={CalculationType.NumberOfMembers}>Number of members</option>
                    </select>
                </div>
                <div className="form-group">
                    <label>Due date</label>
                    <br/>
                    <ReactDatePicker 
                    className="form-control"
                        onChange={this.setDate}
                        selected={moment(this.state.date).toDate()}
                    />
                </div>
                <div className="form-group">
                    <label>Price</label>
                    <input
                        type="number"
                        onChange={(e) => this.setState({price: parseFloat(e.target.value)}) }
                        className="form-control"
                        required
                        defaultValue={this.state.price}
                    />
                </div>
                <button type="submit" className="btn btn-primary">Submit</button>
            </form>
        )
    }
}