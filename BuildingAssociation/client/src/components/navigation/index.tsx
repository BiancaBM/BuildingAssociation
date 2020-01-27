import * as React from 'react';
import './style.css';
import {
    RouteComponentProps,
  } from 'react-router';

import { Link } from 'react-router-dom';

export default class Navigation extends React.Component<RouteComponentProps<any>> {
    logout = () => {
        sessionStorage.clear();
    }
    render() {
        const userName = sessionStorage.getItem('userName');
        const email = sessionStorage.getItem('userEmail');

        return (
            <div className="navigation-container">
                <div>
                    {userName && <span>Hello {`${userName} (${email})`}</span>}
                </div>
                <ul className="navigation-content">
                    <li>
                        <Link to="/">Home</Link>
                    </li>
                    <li>
                        <Link to="/about">About</Link>
                    </li>
                    <li>
                        {sessionStorage.getItem('authToken') !== null 
                            ? <Link to={{
                                pathname: '/',
                                state: {from: 'logout'}
                            }} onClick={this.logout}>Logout</Link>
                            : <Link to="/login">Login</Link>}
                    </li>
                </ul>
            </div>
        )
    }
}