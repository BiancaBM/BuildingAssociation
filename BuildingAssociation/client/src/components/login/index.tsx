import * as React from 'react';
import './style.css';
import * as config from '../../config';
import { Redirect } from 'react-router';

interface LoginState {
    userLogged: boolean;
    showError: boolean;
    userFullName: string,
    username: string
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
        debugger;
        fetch(`${config.apiUrl}/authentication/validate?email=${email}&password=${encodedPassword}`)
        .then(response => {
            if(response.ok) return response.json()
            else return undefined;
        }).then((user: any) => {
                if(user) {
                    const stringToEncode = btoa(`${email}:${encodedPassword}`);
                    sessionStorage.setItem('authToken', `BASIC ${stringToEncode}`);
                    this.setState({userLogged: true, showError: false, username: user.email, userFullName: user.name});
                } else {
                    this.setState({userLogged: false, showError: true, userFullName: undefined, username: undefined});
                    sessionStorage.removeItem('authTocken');
                }
            }
        )
    }

    render() {
        if(this.state.userLogged) {
            return <Redirect to={{
                pathname: '/',
                state: { userFullName: this.state.userFullName, username: this.state.username, from: 'login' },
            }} />
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
                    {this.state.showError && <div>Error!!</div>}
                    <button type="submit" className="btn btn-primary">Submit</button>
                </form>
            </div>
        )
    }
}