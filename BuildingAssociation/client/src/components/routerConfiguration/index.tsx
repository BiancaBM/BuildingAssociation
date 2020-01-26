import * as React from 'react';
import {Switch, Route, withRouter, RouteComponentProps } from 'react-router';
import Navigation from '../navigation';
import About from '../about';
import Login from '../login';
import Main from '../main';
import AddBill from '../addForms/addBill';
import './style.css';
import BillList from '../lists/billList';
import MansionList from '../lists/mansionList';
import AddMansion from '../addForms/addMansion';
import AddProvider from '../addForms/addProvider';
import ProviderList from '../lists/providerList';
import AddUser from '../addForms/addUser';
import UserList from '../lists/userList';
import OtherConsumptionList from '../lists/otherConsumptionList';
import AddApartment from '../addForms/addApartment';
import ApartmentList from '../lists/apartmentList';
import AddOtherConsumption from '../addForms/addOtherConsumption';
import AddWaterConsumption from '../addForms/addWaterConsumption';
import WaterConsumptionList from '../lists/waterConsumptionList';
import ChangePassword from '../addForms/changePassword';

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
    const item = (props: any, component: any) => <>
        <Navigation {...props} />
          <div className="main-container">
            {component}
          </div> 
      </>

      return (
        <div className="root-container">
          <Switch>
            <Route path='/addmansion/:id' exact={false} render={(props) => item(props, <AddMansion {...props}/>)}/>
            <Route path='/addmansion/' exact={false} render={(props) => item(props, <AddMansion {...props}/>)}/>
            
            <Route path='/addbill/:id' exact={false} render={(props) => item(props, <AddBill {...props}/>)}/>
            <Route path='/addbill/' exact={false} render={(props) => item(props, <AddBill {...props}/>)}/>

            <Route path='/addprovider/:id' exact={false} render={(props) => item(props, <AddProvider {...props}/>)}/>
            <Route path='/addprovider/' exact={false} render={(props) => item(props, <AddProvider {...props}/>)}/>

            <Route path='/adduser/:id' exact={false} render={(props) => item(props, <AddUser {...props}/>)}/>
            <Route path='/adduser/' exact={false} render={(props) => item(props, <AddUser {...props}/>)}/>

            <Route path='/addapartment/:id' exact={false} render={(props) => item(props, <AddApartment {...props}/>)}/>
            <Route path='/addapartment/' exact={false} render={(props) => item(props, <AddApartment {...props}/>)}/>

            <Route path='/addconsumption/:id' exact={false} render={(props) => item(props, <AddOtherConsumption {...props}/>)}/>
            <Route path='/addconsumption/' exact={false} render={(props) => item(props, <AddOtherConsumption {...props}/>)}/>

            <Route path='/addwaterconsumption/:id' exact={false} render={(props) => item(props, <AddWaterConsumption {...props}/>)}/>
            <Route path='/addwaterconsumption/' exact={false} render={(props) => item(props, <AddWaterConsumption {...props}/>)}/>
            
            <Route path='/changepassword/' exact={false} render={(props) => item(props, <ChangePassword {...props}/>)}/>

            <Route path='/about' exact={false} render={(props) => item(props, <About {...props}/>)}/>
            <Route path='/login' exact={false} render={(props) => item(props, <Login {...props}/>)}/>
            <Route path='/billlist' exact={false} render={(props) => item(props, <BillList {...props}/>)}/>
            <Route path='/mansions' exact={false} render={(props) => item(props, <MansionList {...props}/>)}/>
            <Route path='/providers' exact={false} render={(props) => item(props, <ProviderList {...props}/>)}/>
            <Route path='/users' exact={false} render={(props) => item(props, <UserList {...props}/>)}/>
            <Route path='/consumptions' exact={false} render={(props) => item(props, <OtherConsumptionList {...props}/>)}/>
            <Route path='/apartments' exact={false} render={(props) => item(props, <ApartmentList {...props}/>)}/>
            <Route path='/waterconsumptions' exact={false} render={(props) => item(props, <WaterConsumptionList {...props}/>)}/>
            <Route path='/' exact={false} render={(props) => item(props, <Main {...props}/>)}/>
          </Switch>
        </div>
    );
  }
}

export const configWithRouter = withRouter(RouterConfiguration);