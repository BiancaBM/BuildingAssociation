import * as React from 'react';
import { RouteComponentProps, Redirect } from 'react-router';
import { User } from '../../models/User';
import { Mansion } from '../../models/Mansion';
import { Apartment } from '../../models/Apartment';

interface AddApartmentState {
    users: User[];
    selectedUser?: User | null;
    mansions: Mansion[];
    selectedMansion?: Mansion;
    floor: number;
    individualQuota: number;
    number: number;
    surface: number;

    saved: boolean;
}

export default class AddApartment extends React.Component<RouteComponentProps<any>, AddApartmentState> {
    constructor(props: RouteComponentProps<any>) {
        super(props);

        this.state = {
            users: [],
            mansions: [],
            saved: false,
            floor: 0,
            individualQuota: 0,
            number:0, 
            surface: 0
        }
    }

    componentDidMount() {
        this.initData();
    }

    initData = async () => {
        if(sessionStorage.getItem('authToken') != null) {
            const usersFromDb = await fetch(`/users`, {
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

            this.setState({mansions: mansionsFromDb, users: usersFromDb});
        }
    }

    submit = (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();

        const item: Apartment = {
            mansionId: this.state.selectedMansion?.id,
            userId: this.state.selectedUser?.userId,
            floor: this.state.floor,
            individualQuota: this.state.individualQuota,
            number: this.state.number,
            surface: this.state.surface,
            userName: this.state.selectedUser?.name as string,
            mansionName: this.state.selectedMansion?.address as string
        }

        fetch('/apartments', {
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
            this.setState({selectedMansion: selectedM, selectedUser: undefined}, () => {
                    const selectUser = document.getElementById('userselect') as HTMLSelectElement;
                    selectUser.selectedIndex = 0;  // first option is selected, or
                                                     // -1 for no option selected
            });
        }
    }

    renderUsers = () => {
        if(this.state.selectedMansion) {
            return this.state.users && 
            this.state.users
                .filter(x => x.mansionId === this.state.selectedMansion?.id)
                .map((user: User) => {
                return <option value={user.userId}>{user.name}</option>
            });
        }

        return this.state.users && 
            this.state.users
                .map((user: User) => {
                return <option value={user.userId}>{user.name}</option>
            });
    }

    selectUser = (e: React.ChangeEvent<HTMLSelectElement>) => {
        const selectedOptionId = parseInt(e.target.selectedOptions[0].value);
        const selectedU = this.state.users.find(x => x.userId === selectedOptionId);

        if(selectedU) {
            this.setState({selectedUser: selectedU});
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
                <h3>Add apartment</h3>
                <label>Mansion</label>
                <select required className="form-control" onChange={this.selectMansion}>
                    <option value="">---</option>
                    {this.renderMansions()}
                </select>
                <label>User</label>
                <select required className="form-control" onChange={this.selectUser} id="userselect">
                    <option value="">---</option>
                    {this.renderUsers()}
                </select>
                <div className="form-group">
                    <label>Number</label>
                    <input
                        type="number"
                        min="0"
                        onChange={(e) => this.setState({number: parseInt(e.target.value)}) }
                        className="form-control"
                        required
                    />
                </div>
                <div className="form-group">
                    <label>Floor</label>
                    <input
                        type="number"
                        min="0"
                        onChange={(e) => this.setState({floor: parseInt(e.target.value)}) }
                        className="form-control"
                        required
                    />
                </div>
                <div className="form-group">
                    <label>Surface</label>
                    <input
                        type="number"
                        step="any"
                        onChange={(e) => this.setState({surface: parseFloat(e.target.value)}) }
                        className="form-control"
                        required
                    />
                </div>
                <div className="form-group">
                    <label>Individual Quota</label>
                    <input
                        type="number"
                        step="any"
                        onChange={(e) => this.setState({individualQuota: parseFloat(e.target.value)}) }
                        className="form-control"
                        required
                    />
                </div>
                <button type="submit" className="btn btn-primary">Submit</button>
            </form>
        )
    }
}