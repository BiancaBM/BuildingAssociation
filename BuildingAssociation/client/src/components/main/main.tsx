import * as React from "react";
import * as config from '../../config';
import './style.css';

interface MainState {
    users: any;
}

export default class Main extends React.Component<{}, MainState> {
    constructor(props: any){
        super(props);

        this.state = {
            users: []
        }
    }

    componentDidMount() {
        fetch(`${config.apiUrl}/Users`).then(response => {
              if(response.ok) {
                  return response.json();
              }

              return undefined;
          }).then(result => this.setState({users: result}));
    }

    renderUsers = () => this.state.users.map((user: any, index: number) => (
        <div key={`user-${index}`} className="user-container">
            <span>Name: {user.name}</span>
            <span>Email: {user.email}</span>
        </div>
    ))

    render() {
        return (
            <div>{this.renderUsers()}</div>
        )
    }
}