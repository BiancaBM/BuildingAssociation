import * as React from 'react';
import { RouteComponentProps, Redirect } from 'react-router';
import { Mansion } from '../../../models/Mansion';

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

    componentDidMount(){
         const { id } = this.props.match.params;
         if(id) {
             this.getItem(id);
         }
    }

    getItem = (id: number) => {
        if(sessionStorage.getItem('authToken') != null) {
            fetch(`/mansions/${id}`, {
                headers: {
                    'Authorization': sessionStorage.getItem('authToken')
                } 
            } as RequestInit).then(response => {
                if(response.ok) {
                return response.json();
                }

                return undefined;
            }).then((result: Mansion) => {
                this.setState({ totalFunds: result.totalFunds as number, address: result.address });
            });
        }
    }

    submit = (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();

        const mansion: Mansion = {
           address: this.state.address,
           totalFunds: this.state.totalFunds,
           bills: [],
           users: [],
           consumptions: [],
           id: this.props.match.params.id
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
           return <Redirect to="/mansions" /> 
        }

        return (
            <form className="container addmansion-container" onSubmit={this.submit}>
                <h3>Add mansion</h3>
                <div className="form-group">
                    <label>Address</label>
                    <input
                        type="text"
                        onChange={(e) => this.setState({address: e.target.value}) }
                        className="form-control"
                        value={this.state.address}
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
                        value={this.state.totalFunds}
                        required
                    />
                </div>
                <button type="submit" className="btn btn-primary">Submit</button>
            </form>
        )
    }
}