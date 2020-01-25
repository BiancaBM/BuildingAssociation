import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import { Provider } from '../../../models/Provider';
import { BootstrapTable, TableHeaderColumn } from 'react-bootstrap-table';
import { Link } from 'react-router-dom';

interface ProviderListState {
    providers: Provider[];
}

export default class ProviderList extends React.Component<RouteComponentProps<any>, ProviderListState> {
    constructor(props: RouteComponentProps<any>) {
        super(props);

        this.state = {
            providers: [],
        }
    }

    componentDidMount() {
        this.initData();
    }

    initData = () => {
        if(sessionStorage.getItem('authToken') != null) {
            fetch(`/providers`, {
                headers: {
                    'Authorization': sessionStorage.getItem('authToken')
                } 
            } as RequestInit).then(response => {
                if(response.ok) {
                return response.json();
                }

                return undefined;
            }).then((result: Provider[]) => {
                this.setState({ providers: result })
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
            <div className="container providerlist-container">
                <Link to={'/addprovider'} className="btn btn-info">Add provider</Link>

                <BootstrapTable data={this.state.providers} containerClass="mt-3"
                    striped hover
                    version='4'
                    search
                    exportCSV
                    options={{
                        noDataText: 'You have no provider!' ,
                        defaultSortName: 'name',
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
                    <TableHeaderColumn isKey hidden dataField='providerId'>Provider ID</TableHeaderColumn>
                    <TableHeaderColumn dataField='name' filter={ { type: 'TextFilter' } } dataSort={true}>Provider Name</TableHeaderColumn>
                    <TableHeaderColumn dataField='cui' filter={ { type: 'TextFilter' } } dataSort={true}>CUI</TableHeaderColumn>
                    <TableHeaderColumn dataField='bankAccount' filter={ { type: 'TextFilter' } } dataSort={true}>Bank Account</TableHeaderColumn>
                    <TableHeaderColumn
                        dataField='unitPrice'
                        dataSort={true}
                        filter={ { type: 'NumberFilter', delay: 1000, numberComparators: [ '=', '>', '<=' ] } }
                    >Unit Price</TableHeaderColumn>
                </BootstrapTable>
            </div>
        )
    }
}