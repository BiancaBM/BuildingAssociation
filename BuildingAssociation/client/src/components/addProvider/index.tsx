import * as React from 'react';
import { RouteComponentProps, Redirect } from 'react-router';
import { Provider } from '../../models/Provider';

interface AddProviderState {
    CUI: string;
    unitPrice: number;
    name: string;
    bankAccount: string;
    saved: boolean;
}

export default class AddProvider extends React.Component<RouteComponentProps<any>, AddProviderState> {
    constructor(props: RouteComponentProps<any>) {
        super(props);

        this.state = {
            CUI: "",
            bankAccount: "",
            name: "",
            unitPrice: 0,
            saved: false
        }
    }

    submit = (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();

        const provider: Provider = {
            bankAccount: this.state.bankAccount,
            cui: this.state.CUI,
            name: this.state.name,
            unitPrice: this.state.unitPrice
        }

        fetch('/providers', {
            method: 'POST',
            body: JSON.stringify(provider),
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
                    <label>Provider Name</label>
                    <input
                        type="text"
                        onChange={(e) => this.setState({name: e.target.value}) }
                        className="form-control"
                        required
                    />
                </div>
                <div className="form-group">
                    <label>CUI</label>
                    <input
                        type="text"
                        onChange={(e) => this.setState({CUI: e.target.value}) }
                        className="form-control"
                    />
                </div>
                <div className="form-group">
                    <label>Bank Account</label>
                    <input
                        type="text"
                        onChange={(e) => this.setState({bankAccount: e.target.value}) }
                        className="form-control"
                        required
                    />
                </div>
                <div className="form-group">
                    <label>Unit Price</label>
                    <input 
                        type="number"
                        step="any"
                        onChange={(e) => this.setState({unitPrice: parseFloat(e.target.value)}) }
                        className="form-control"
                        required
                    />
                </div>
                <button type="submit" className="btn btn-primary">Submit</button>
            </form>
        )
    }
}