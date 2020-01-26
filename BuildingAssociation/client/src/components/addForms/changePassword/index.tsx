import * as React from 'react';
import { RouteComponentProps, Redirect } from 'react-router';
import { Password } from '../../../models/Password';

interface ChangePasswordState {
    password?: string;
    confirmPassword?: string;
    confirmationError: boolean;
    saved: boolean;
}

export default class ChangePassword extends React.Component<RouteComponentProps<any>, ChangePasswordState> {
    constructor(props: RouteComponentProps<any>) {
        super(props);

        this.state = {
            confirmationError: false,
            saved: false
        }
    }

    submit = (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        
        if(this.state.password !== this.state.confirmPassword) {
            this.setState({confirmationError: true});
            return;
        } 
         
        const passw: Password = {
            password: this.state.password as string
        }

        fetch('/password', {
            method: 'POST',
            body: JSON.stringify(passw),
            headers: {
                'Content-Type': 'application/json',
                'Authorization': sessionStorage.getItem('authToken')
            }
        } as RequestInit).then(result => { 
            this.setState({saved: true}, () => {
                sessionStorage.clear();
                window.location.reload();
            });
        });
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
                <h3>Change password</h3>
                <div className="form-group">
                        <label>Password</label>
                        <input
                            type="password"
                            className="form-control"
                            onChange={(e) => this.setState({password: btoa(e.target.value)}) }
                            required
                        />
                    </div>
                <div className="form-group">
                        <label>Confirm Password</label>
                        <input
                            type="password"
                            className="form-control"
                            onChange={(e) => this.setState({confirmPassword: btoa(e.target.value)}) }
                            required
                            id="confirm_password"
                        />
                    </div>
                {this.state.confirmationError ? <div style={{color: 'red'}}>Don't match!</div>: undefined}
                <br/>
                <button type="submit" className="btn btn-primary">Submit</button>
            </form>
        )
    }
}