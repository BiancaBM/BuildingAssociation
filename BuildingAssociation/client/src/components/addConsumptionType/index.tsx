import * as React from 'react';
import { RouteComponentProps, Redirect } from 'react-router';
import { Mansion } from '../../models/Mansion';
import { ConsumptionType } from '../../models/ConsumptionType';
import moment from 'moment';
import { CalculationType } from '../../models/CalculationType';
import ReactDatePicker from 'react-datepicker';

interface AddConsumptionTypeState {
    name: string;
    calculationType: number;
    date: string;
    saved: boolean;
    mansions: Mansion[];
    selectedMansion?: Mansion;
}

export default class AddConsumptionType extends React.Component<RouteComponentProps<any>, AddConsumptionTypeState> {
    constructor(props: RouteComponentProps<any>) {
        super(props);

        this.state = {
            name: "",
            calculationType: 0,
            date: moment.utc().startOf('day').toISOString(),
            saved: false,
            mansions: [],
            selectedMansion: undefined
        }
    }

    componentDidMount() {
        fetch(`/mansions`, {
            headers: {
                'Authorization': sessionStorage.getItem('authToken')
            } 
        } as RequestInit).then(response => {
            if(response.ok) {
            return response.json();
            }

            return undefined;
        }).then(result => this.setState({mansions: result}));
    }

    submit = (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();

        const item: ConsumptionType = {
            name: this.state.name,
            calculationType: this.state.calculationType,
            date: this.state.date,
            mansionId: this.state.selectedMansion?.id,
            mansionName: this.state.selectedMansion?.address as string
        }

        fetch('/consumptiontype', {
            method: 'POST',
            body: JSON.stringify(item),
            headers: {
                'Content-Type': 'application/json',
                'Authorization': sessionStorage.getItem('authToken')
            }
        } as RequestInit).then(result => { this.setState({saved: true})});
    }

    renderMansions = () => {
        return this.state.mansions && this.state.mansions.map((mansion: Mansion) => {
            return <option value={mansion.id}>{mansion.address}</option>
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
            <form className="container addconsumptiontype-container" onSubmit={this.submit}>
                <h3>Add consumption type</h3>
                <div className="form-group">
                    <label>Name</label>
                    <input
                        type="text"
                        onChange={(e) => this.setState({name: e.target.value}) }
                        className="form-control"
                        required
                    />
                </div>
                <label>Mansion</label>
                <select required className="form-control" onChange={this.selectMansion}>
                    <option value="">---</option>
                    {this.renderMansions()}
                </select>
                <div className="form-group">
                    <label>Calculation type</label>
                    <select className="form-control" onChange={this.selectType} required>
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
                <button type="submit" className="btn btn-primary">Submit</button>
            </form>
        )
    }
}