import * as React from 'react';
import {Switch, Route, withRouter, RouteComponentProps } from 'react-router';
import Navigation from '../navigation';
import About from '../about';
import Login from '../login';
import Main from '../main';

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
            <div>
              <Switch>
                <Route path="/about">
                  <Navigation {...this.props} />
                  <About />
                </Route>
                <Route path="/login">
                  <Navigation {...this.props}/>
                  <Login />
                </Route>
                <Route path="/">
                  <Navigation {...this.props}/>
                  <Main {...this.props} />
                </Route>
              </Switch>
            </div>
        );
    }
}

export const configWithRouter = withRouter(RouterConfiguration);