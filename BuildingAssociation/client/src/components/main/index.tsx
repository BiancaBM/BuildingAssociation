import * as React from 'react';
import './style.css';
import { User } from '../../models/User';
import { RouteComponentProps } from 'react-router';
import { Link } from 'react-router-dom';


interface MainState {
    users: User[];
}

export default class Main extends React.Component<RouteComponentProps<any>, MainState> {
    constructor(props: any){
        super(props);

        this.state = {
            users: []
        }
    }

    componentDidMount() {
        this.initData();
    }

    initData = () => {
        if(sessionStorage.getItem('authToken') != null) {
            fetch(`/Users`, {
                headers: {
                    'Authorization': sessionStorage.getItem('authToken')
                } 
            } as RequestInit).then(response => {
                if(response.ok) {
                return response.json();
                }

                return undefined;
            }).then(result => this.setState({users: result }));
        }
    }

    renderUsers = () => {
        if(!this.state.users) return undefined;

        return this.state.users.map((user: User) => {
            return <div>
                <span>User: {user.name}</span>
            </div>
        })
    }

    render() {
        if(sessionStorage.getItem('authToken') == null) {
            return <div>
                Nu esti logat!
            </div>
        }

        return (
            <>
            {this.renderUsers()}
            <Link to={'/mansions'}>Mansions</Link>
            <Link to={'/billlist'}>Bills</Link>
            <Link to={'/providers'}>Providers</Link>
            <Link to={'/users'}>Users</Link>
            </>
        )
    }
}