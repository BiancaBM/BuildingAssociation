import * as React from 'react';
import './style.css';
import { Redirect } from 'react-router';
import { User } from '../../models/User';

interface LoginState {
    userLogged: boolean;
    showError: boolean;
    userFullName?: string,
    username?: string
}
export default class Login extends React.Component<{}, LoginState> {
    constructor(props: Readonly<{}>) {
        super(props);

        this.state = {
            userLogged: sessionStorage.getItem('authToken') !== null,
            showError: false,
            userFullName: undefined,
            username: undefined
        }
    }

    submit = (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        const email = (document.getElementById('emailfield') as HTMLInputElement).value;
        const password = (document.getElementById('passwordfield') as HTMLInputElement).value;
        const encodedPassword = btoa(password);

        fetch(`/authentication/validate?email=${email}&password=${encodedPassword}`)
        .then(response => response.json())
        .then((result: User | string) => {
            if(typeof result == 'string') {
                this.setState({userLogged: false}, () => {
                    sessionStorage.clear();
                    alert(result);
                });
            } else {
                const user = result as User;
                const stringToEncode = btoa(`${email}:${encodedPassword}`);
                sessionStorage.setItem('authToken', `BASIC ${stringToEncode}`);
                sessionStorage.setItem('isAdmin', `${user.isAdmin}`);
                sessionStorage.setItem('userName', `${user.name}`);
                sessionStorage.setItem('userEmail', `${user.email}`);
                this.setState({userLogged: true});
            }      
        });
    }

    render() {
        if(this.state.userLogged) {
            return <Redirect to={{ pathname: '/' }} />
        }

        return (
            <div className="about-container">
                <form className="about-details" onSubmit={this.submit}>
                    <div className="form-group">
                        <label>Email address</label>
                        <input type="email" className="form-control" id="emailfield" aria-describedby="emailHelp" placeholder="Enter email"/>
                        <small id="emailHelp" className="form-text text-muted">We'll never share your email with anyone else.</small>
                    </div>
                    <div className="form-group">
                        <label>Password</label>
                        <input type="password" className="form-control" id="passwordfield" placeholder="Password"/>
                    </div>
                    {this.state.showError && <div style={{color: 'red'}}>Invalid!</div>}
                    <button type="submit" className="btn btn-primary">Submit</button>
                </form>
            </div>
        )
    }
}