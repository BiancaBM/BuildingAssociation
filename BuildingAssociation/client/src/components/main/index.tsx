import * as React from 'react';
import './style.css';
import { RouteComponentProps } from 'react-router';
import { Link } from 'react-router-dom';

export default class Main extends React.Component<RouteComponentProps<any>> {

    render() {
        if(sessionStorage.getItem('authToken') == null) {
            return <div>
                Nu esti logat!
            </div>
        }

        const adminMenu = 
            <>
                <Link to={'/mansions'}>Mansions</Link>
                <Link to={'/billlist'}>Bills</Link>
                <Link to={'/providers'}>Providers</Link>
                <Link to={'/users'}>Users</Link>
                <Link to={'/consumptions'}>Consumptions</Link>
                <Link to={'/apartments'}>Apartments</Link>
                <Link to={'/waterconsumptions'}>Water consumptions</Link>
                <Link to={'/generatedbills'}>Generated bills</Link>
                <Link to={'/changepassword'}>Change password</Link>
            </>
                

        const userMenu = 
            <>
                <Link to={'/waterconsumptions'}>Water consumptions</Link>
                <Link to={'/generatedbills'}>Generated bills</Link>
                <Link to={'/changepassword'}>Change password</Link>
            </>

        const isAdmin = window.sessionStorage.getItem('isAdmin') === 'true';

        return (
            <>
                { isAdmin ? adminMenu : userMenu}
            </>
        )
    }
}