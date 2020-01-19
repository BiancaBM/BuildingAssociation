import * as React from 'react';
import { RouteComponentProps, Redirect } from 'react-router';
import { Mansion } from '../../models/Mansion';

interface AddMansionState {
    address: string;
    totalFunds: number;
    saved: boolean;
}

export default class AddMansion extends React.Component<RouteComponentProps<any>, AddMansionState> {
    constructor(props: RouteComponentProps<any>) {
        super(props);

        this.state = {
            address: "",
            totalFunds: 0,
            saved: false
        }
    }

    submit = (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();

        const mansion: Mansion = {
           address: this.state.address,
           totalFunds: this.state.totalFunds,
           bills: [],
           users: []
        }

        fetch('/mansions', {
            method: 'POST',
            body: JSON.stringify(mansion),
            headers: {
                'Content-Type': 'application/json',
                'Authorization': sessionStorage.getItem('authToken')
            }
        } as RequestInit).then(result => { this.setState({saved: true})});
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
            <form className="container addmaison-container" onSubmit={this.submit}>
                <div className="form-group">
                    <label>Address</label>
                    <input
                        type="text"
                        onChange={(e) => this.setState({address: e.target.value}) }
                        className="form-control"
                        required
                    />
                </div>
                <div className="form-group">
                    <label>Total Funds</label>
                    <input 
                        type="number"
                        step="any"
                        onChange={(e) => this.setState({totalFunds: parseFloat(e.target.value)}) }
                        className="form-control"
                        required
                    />
                </div>
                <button type="submit" className="btn btn-primary">Submit</button>
            </form>
        )
    }
}