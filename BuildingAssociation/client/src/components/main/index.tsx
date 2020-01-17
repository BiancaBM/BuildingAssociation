import * as React from 'react';
import * as config from '../../config';
import './style.css';
import { User } from '../../models/user';
import { RouteComponentProps } from 'react-router';


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
            fetch(`${config.apiUrl}/Users`, {
                headers: {
                    'Authorization': sessionStorage.getItem('authToken')
                }
            }).then(response => {
                if(response.ok) {
                return response.json();
                }

                return undefined;
            }).then(result => this.setState({users: result }, () => {debugger;}));
        }
    }

    /*shouldComponentUpdate(nextProps: RouteComponentProps<any>) {
        const nextLocationState = nextProps.location.state as any;
        const locationState = this.props.location.state as any;
        return locationState 
            && locationState.from
            && nextLocationState
            && nextLocationState.from
            && nextLocationState.from !== locationState.from;
    }*/


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
            <>{this.renderUsers()}</>
        )
    }
}