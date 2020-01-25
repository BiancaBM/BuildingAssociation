import * as React from 'react';
import { RouteComponentProps, Redirect } from 'react-router';
import { Provider } from '../../../models/Provider';

interface AddProviderState {
    CUI: string;
    unitPrice?: number;
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
            unitPrice: undefined,
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
        fetch(`/providers/${id}`, {
            headers: {
                'Authorization': sessionStorage.getItem('authToken')
            } 
        } as RequestInit).then(response => {
            if(response.ok) {
            return response.json();
            }

            return undefined;
        }).then((result: Provider) => {
            this.setState({ 
                CUI: result.cui,
                bankAccount: result.bankAccount,
                name: result.name,
                unitPrice: result.unitPrice
             });
        });
    }
}

    submit = (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();

        const provider: Provider = {
            bankAccount: this.state.bankAccount,
            cui: this.state.CUI,
            name: this.state.name,
            unitPrice: this.state.unitPrice as number,
            providerId: this.props.match.params.id
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
           return <Redirect to="/providers" /> 
        }

        return (
            <form className="container addprovider-container" onSubmit={this.submit}>
                <h3>Add provider</h3>
                <div className="form-group">
                    <label>Provider Name</label>
                    <input
                        type="text"
                        onChange={(e) => this.setState({name: e.target.value}) }
                        className="form-control"
                        required
                        value={this.state.name}
                    />
                </div>
                <div className="form-group">
                    <label>CUI</label>
                    <input
                        type="text"
                        onChange={(e) => this.setState({CUI: e.target.value}) }
                        className="form-control"
                        value={this.state.CUI}
                    />
                </div>
                <div className="form-group">
                    <label>Bank Account</label>
                    <input
                        type="text"
                        onChange={(e) => this.setState({bankAccount: e.target.value}) }
                        className="form-control"
                        required
                        value={this.state.bankAccount}
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
                        value={this.state.unitPrice}
                    />
                </div>
                <button type="submit" className="btn btn-primary">Submit</button>
            </form>
        )
    }
}