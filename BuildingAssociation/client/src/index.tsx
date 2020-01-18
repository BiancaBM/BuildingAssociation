import * as React from "react";
import * as ReactDOM from "react-dom";
import 'bootstrap/dist/css/bootstrap.css';
import { BrowserRouter as Router } from 'react-router-dom';

import { configWithRouter as ConfigWithRouter } from './components/routerConfiguration';
class App extends React.Component {

  render() {
    return (
      <Router>
          <div>
              <ConfigWithRouter />
          </div>
      </Router>
    )
  }
  
};

ReactDOM.render(<App />, document.getElementById("root"));