import * as React from 'react';
import { RouteComponentProps, Redirect } from 'react-router';
import { Mansion } from '../../../models/Mansion';
import { User } from '../../../models/User';

interface AddUserState {
    name: string;
	email: string;
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
            name: "",
            password: "",
            selectedMansion: undefined,
            saved: false
        }
    }

    componentDidMount() {
        const { id } = this.props.match.params;
        this.initData(id);
    }

    initData = async (id?: number) => {
        if(sessionStorage.getItem('authToken') != null) {
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
                const item: User = await fetch(`/users/${id}`, {
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
                        email: item.email,
                        name: item.name,
                        selectedMansion: mansionsFromDb.find(x => x.id === item.mansionId),
                        password: item.password,
                    })
                }
            } else {
                this.setState({
                    mansions: mansionsFromDb,
                });
            }
        }
    }

    submit = (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();

        const user: User = {
            name: this.state.name,
            email: this.state.email,
            mansionId: this.state.selectedMansion?.id,
            mansionName: this.state.selectedMansion?.address as string,
            apartments:[],
            waterConsumptions: [],
            password: this.state.password,
            userId: this.props.match.params.id
        }

        fetch('/users', {
            method: 'POST',
            body: JSON.stringify(user),
            headers: {
                'Content-Type': 'application/json',
                'Authorization': sessionStorage.getItem('authToken')
            }
        } as RequestInit).then(result => {
            if(result.ok) {
                this.setState({saved: true});
                return;
            }
            return result.json();
        }).then(error => {
            if(error) alert(error);
        });
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

    render() {
        if(sessionStorage.getItem('authToken') == null) {
            return <div>
                Nu esti logat!
            </div>
        }

        if(this.state.saved)
        {
           return <Redirect to="/users" /> 
        }

        const formTitle = `${this.props.match.params.id ? "Modify" : "Add" } user`;

        return (
            <form className="container addmaison-container" onSubmit={this.submit}>
                <h3>{formTitle}</h3>
                <label>Mansion</label>
                <select required className="form-control" onChange={this.selectMansion} defaultValue={this.state.selectedMansion?.id}>
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
                        defaultValue={this.state.name}
                    />
                </div>
                <div className="form-group">
                    <label>Email</label>
                    <input
                        type="email"
                        onChange={(e) => this.setState({email: e.target.value}) }
                        className="form-control"
                        required
                        readOnly={this.props.match.params.id}
                        defaultValue={this.state.email}
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