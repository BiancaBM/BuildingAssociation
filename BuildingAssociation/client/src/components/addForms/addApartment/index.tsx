import * as React from 'react';
import { RouteComponentProps, Redirect } from 'react-router';
import { User } from '../../../models/User';
import { Mansion } from '../../../models/Mansion';
import { Apartment } from '../../../models/Apartment';

interface AddApartmentState {
    users: User[];
    selectedUser?: User | null;
    mansions: Mansion[];
    selectedMansion?: Mansion;
    floor?: number;
    individualQuota?: number;
    number?: number;
    surface?: number;
    membersCount?: number;
    saved: boolean;
}

export default class AddApartment extends React.Component<RouteComponentProps<any>, AddApartmentState> {
    constructor(props: RouteComponentProps<any>) {
        super(props);

        this.state = {
            users: [],
            mansions: [],
            saved: false,
            floor: undefined,
            individualQuota: undefined,
            number: undefined, 
            surface: undefined,
            membersCount: undefined
        }
    }

    componentDidMount() {
        const { id } = this.props.match.params;
        this.initData(id);
    }

    initData = async (id?: number) => {
        if(sessionStorage.getItem('authToken') != null) {
            
            const usersFromDb: User[] = await fetch(`/users`, {
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
                const item: Apartment = await fetch(`/apartments/${id}`, {
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
                        users: usersFromDb,
                        floor: item.floor,
                        individualQuota: item.individualQuota,
                        number: item.number,
                        surface: item.surface,
                        selectedMansion: mansionsFromDb.find(x => x.id === item.mansionId),
                        selectedUser: usersFromDb.find(x => x.userId === item.userId),
                        membersCount: item.membersCount
                    })
                }
            } else {
                this.setState({mansions: mansionsFromDb, users: usersFromDb});
            }
        }
    }

    submit = (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();

        const item: Apartment = {
            mansionId: this.state.selectedMansion?.id,
            userId: this.state.selectedUser?.userId,
            floor: this.state.floor as number,
            individualQuota: this.state.individualQuota as number,
            number: this.state.number as number,
            surface: this.state.surface as number,
            userName: this.state.selectedUser?.name as string,
            mansionName: this.state.selectedMansion?.address as string,
            apartmentId: this.props.match.params.id,
            membersCount: this.state.membersCount as number
        }

        fetch('/apartments', {
            method: 'POST',
            body: JSON.stringify(item),
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
            const isSelected = this.state.selectedMansion && this.state.selectedMansion?.id === mansion.id;
            return <option key={`${mansion.id}-${index}`} value={mansion.id} selected={isSelected}>{mansion.address}</option>
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
                .map((user: User, index: number) => {
                    const isSelected = this.state.selectedUser?.userId === user.userId;
                    return <option key={`${user.userId}-${index}`} value={user.userId} selected={isSelected}>{user.name}</option>
            });
        }

        return this.state.users && 
            this.state.users
                .map((user: User, index: number) => {
                return <option key={`${user.userId}-${index}`} value={user.userId}>{user.name}</option>
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
           return <Redirect to="/apartments" /> 
        }

        const formTitle = `${this.props.match.params.id ? "Modify" : "Add" } appartment`;

        return (
            <form className="container addmaison-container" onSubmit={this.submit}>
                <h3>{formTitle}</h3>
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
                    <label>Member count</label>
                    <input 
                        type="number"
                        min="0"
                        onChange={(e) => this.setState({membersCount: parseFloat(e.target.value)}) }
                        className="form-control"
                        required
                        defaultValue={this.state.membersCount}
                    />
                </div>
                <div className="form-group">
                    <label>Number</label>
                    <input
                        type="number"
                        min="0"
                        onChange={(e) => this.setState({number: parseInt(e.target.value)}) }
                        className="form-control"
                        required
                        defaultValue={this.state.number}
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
                        defaultValue={this.state.floor}
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
                        defaultValue={this.state.surface}
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
                        defaultValue={this.state.individualQuota}
                    />
                </div>
                <button type="submit" className="btn btn-primary">Submit</button>
            </form>
        )
    }
}