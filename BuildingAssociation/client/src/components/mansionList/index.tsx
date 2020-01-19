import * as React from 'react';
import './style.css';
import { RouteComponentProps } from 'react-router';
import { Mansion } from '../../models/Mansion';
import { BootstrapTable, TableHeaderColumn } from 'react-bootstrap-table';
import { Link } from 'react-router-dom';

interface MansionListState {
    mansions: Mansion[];
}

export default class MansionList extends React.Component<RouteComponentProps<any>, MansionListState> {
    constructor(props: RouteComponentProps<any>) {
        super(props);

        this.state = {
            mansions: [],
        }
    }

    componentDidMount() {
        this.initData();
    }

    initData = () => {
        if(sessionStorage.getItem('authToken') != null) {
            fetch(`/mansions`, {
                headers: {
                    'Authorization': sessionStorage.getItem('authToken')
                } 
            } as RequestInit).then(response => {
                if(response.ok) {
                return response.json();
                }

                return undefined;
            }).then((result: Mansion[]) => {
                this.setState({ mansions: result })
            });
        }
    }
    
    render() {
        if(sessionStorage.getItem('authToken') == null) {
            return <div>
                Nu esti logat!
            </div>
        }

        return (
            <div className="container mansonlist-container">
                <Link to={'/addmansion'} className="btn btn-info">Add manson</Link>

                <BootstrapTable data={this.state.mansions} containerClass="mt-3"
                    striped hover
                    version='4'
                    options={{
                        noDataText: 'No mansions to display!' ,
                        defaultSortName: 'address',
                        defaultSortOrder: 'desc',
                        sortIndicator: false,
                        sizePerPage: 5, 
                        sizePerPageList: [ {
                            text: '5', value: 5
                          }, {
                            text: '10', value: 10
                          }, {
                            text: '25', value: 25
                          } ],
                     }}
                     pagination
                >
                    <TableHeaderColumn isKey hidden dataField='id'>Mansion ID</TableHeaderColumn>
                    <TableHeaderColumn dataField='address'>Address</TableHeaderColumn>
                    <TableHeaderColumn
                        dataField='totalFunds'
                        dataSort={true}
                        filter={ { type: 'NumberFilter', delay: 1000, numberComparators: [ '=', '>', '<=' ] } }
                    >Total Funds</TableHeaderColumn>
                </BootstrapTable>
            </div>
        )
    }
}