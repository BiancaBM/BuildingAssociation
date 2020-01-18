import * as React from 'react';
import {Switch, Route, withRouter, RouteComponentProps } from 'react-router';
import Navigation from '../navigation';
import About from '../about';
import Login from '../login';
import Main from '../main';
import AddBill from '../addBill';
import './style.css';
import BillList from '../billList';

class RouterConfiguration extends React.Component<RouteComponentProps<any>> {
    private previousRouteHash: string;

    constructor(props: RouteComponentProps<any>) {
        super(props);
        this.previousRouteHash = "";
    }

    componentDidUpdate() {
        window.scrollTo(0, 0);
        // reload page when coming back from another non training page
        if (this.previousRouteHash !== "" && this.props.location.hash === "") {
            this.previousRouteHash = this.props.location.hash;
            window.location.reload();
        }

        this.previousRouteHash = this.props.location.hash;
    }

    render() {
        return (
            <div className="root-container">
              <Switch>
                <Route path="/about">
                  <Navigation {...this.props} />
                  <div className="main-container">
                    <About />
                  </div>
                </Route>
                <Route path="/login">
                  <Navigation {...this.props}/>
                  <div className="main-container">
                    <Login />
                  </div>
                </Route>
                <Route path="/addbill">
                  <Navigation {...this.props}/>
                  <div className="main-container">
                    <AddBill {...this.props} />
                  </div>
                </Route>
                <Route path="/billlist">
                  <Navigation {...this.props}/>
                  <div className="main-container">
                    <BillList {...this.props} />
                  </div>
                </Route>
                <Route path="/">
                  <Navigation {...this.props}/>
                  <div className="main-container">
                    <Main {...this.props} />
                  </div>
                </Route>
              </Switch>
            </div>
        );
    }
}

export const configWithRouter = withRouter(RouterConfiguration);