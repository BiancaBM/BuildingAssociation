import * as React from 'react';
import './style.css';
import {
    RouteComponentProps,
  } from 'react-router';

import { Link } from 'react-router-dom';

interface NavigationProps
{
    userFullName?: string;
    username?: string;
}

export default class Navigation extends React.Component<RouteComponentProps<any> & NavigationProps> {
    logout = () => {
        sessionStorage.clear();
    }
    render() {
        const locationState: any = this.props.location.state;

        return (
            <div className="navigation-container">
                <div>
                    {locationState && locationState.userFullName && <span>Hello {`${locationState.userFullName} (${locationState.username})`}</span>}
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