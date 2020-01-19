import * as React from 'react';
import { RouteComponentProps, Redirect } from 'react-router';
import { User } from '../../models/User';
import { Mansion } from '../../models/Mansion';

interface AddUserState {
    name: string;
	email: string;
    membersCount: number;
    password: string;
    mansions: Mansion[],
    selectedMansion?: Mansion,
    saved: boolean;
}

export default class AddUser extends React.Component<RouteComponentProps<any>, AddUserState> {
    constructor(props: RouteComponentProps<any>) {
        super(props);

        this.state = {
            mansions: [],
            email: "",
            membersCount: 0,
            name: "",
            password: "",
            selectedMansion: undefined,
            saved: false
        }
    }

    componentDidMount() {
        this.initData();
    }

    initData = () => {
        if(sessionStorage.getItem('authToken') != null) {
            fetch(`/mansions`, {
                headers: {
                    'Authorization': sessionStorage.getItem('authToken')
                } 
            } as RequestInit).then(response => {
                if(response.ok) {
                return response.json();
                }

                return undefined;
            }).then(result => this.setState({mansions: result }));
        }
    }

    submit = (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();

        const user: User = {
            name: this.state.name,
            email: this.state.email,
            membersCount: this.state.membersCount,
            mansionId: this.state.selectedMansion?.id,
            mansionName: this.state.selectedMansion?.address as string,
            apartments:[],
            waterConsumptions: [],
            password: this.state.password
        }

        fetch('/users', {
            method: 'POST',
            body: JSON.stringify(user),
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
                <label>Mansion</label>
                <select required className="form-control" onChange={this.selectMansion}>
                    <option value="">---</option>
                    {this.renderMansions()}
                </select>
                <div className="form-group">
                    <label>Name</label>
                    <input
                        type="text"
                        onChange={(e) => this.setState({name: e.target.value}) }
                        className="form-control"
                        required
                    />
                </div>
                <div className="form-group">
                    <label>Email</label>
                    <input
                        type="email"
                        onChange={(e) => this.setState({email: e.target.value}) }
                        className="form-control"
                        required
                    />
                </div>
                <div className="form-group">
                    <label>Member count</label>
                    <input 
                        type="number"
                        min="0"
                        onChange={(e) => this.setState({membersCount: parseFloat(e.target.value)}) }
                        className="form-control"
                        required
                    />
                </div>
                <div className="form-group">
                        <label>Password</label>
                        <input
                            type="password"
                            className="form-control"
                            onChange={(e) => this.setState({password: btoa(e.target.value)}) }
                            required
                        />
                    </div>
                <button type="submit" className="btn btn-primary">Submit</button>
            </form>
        )
    }
}