import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import { Provider } from '../../../models/Provider';
import { BootstrapTable, TableHeaderColumn } from 'react-bootstrap-table';
import { Link } from 'react-router-dom';
import { ProviderType } from '../../../models/ProviderType';

interface ProviderListState {
    providers: Provider[];
    reload: boolean;
}

export default class ProviderList extends React.Component<RouteComponentProps<any>, ProviderListState> {
    constructor(props: RouteComponentProps<any>) {
        super(props);

        this.state = {
            providers: [],
            reload: false
        }
    }

    componentDidMount() {
        this.initData();
    }

    componentDidUpdate(prevProps: any, prevState: ProviderListState) {
        if(prevState.reload !== this.state.reload && this.state.reload) {
            this.initData();
        }
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
                this.setState({ providers: result, reload: false })
            });
        }
    }

    deleteRow = (item: Provider) => {
        if(sessionStorage.getItem('authToken') != null) {
            fetch(`/providers/${item.providerId}`, {
                    method: 'DELETE',
                    headers: {
                        'Authorization': sessionStorage.getItem('authToken')
                    },
                } as RequestInit).then(response => {
                if (response.ok) {
                    this.setState({reload: true});
                }
            });
        }
    }

    actionsFormatter = (cell: any, row: Provider) => {
        return <>
            <Link to={`/addprovider/${row.providerId}`} className="fas fa-edit"></Link>
            <i
                className="fas fa-trash-alt ml-3"
                onClick={() => this.deleteRow(row)}></i>
        </>;
    }

    enumFormatter = (cell: any, row: any, enumObject: any) => {
        return enumObject[cell];
    }
    
    render() {
        if(sessionStorage.getItem('authToken') == null) {
            return <div>
                Nu esti logat!
            </div>
        }

        const type = {
            [ProviderType.Electricity]: 'Electricity',
            [ProviderType.Water]: 'Water',
            [ProviderType.Other]: 'Other'
        };

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
                    <TableHeaderColumn
                        dataField='type'
                        dataSort={true} filterFormatted dataFormat={ this.enumFormatter }
                        formatExtraData={ type }
                        filter={ { type: 'SelectFilter', options: type, condition: 'eq' } }
                    >Type</TableHeaderColumn>
                    <TableHeaderColumn dataField='cui' filter={ { type: 'TextFilter' } } dataSort={true}>CUI</TableHeaderColumn>
                    <TableHeaderColumn dataField='bankAccount' filter={ { type: 'TextFilter' } } dataSort={true}>Bank Account</TableHeaderColumn>
                    <TableHeaderColumn
                        dataField='unitPrice'
                        dataSort={true}
                        filter={ { type: 'NumberFilter', delay: 1000, numberComparators: [ '=', '>', '<=' ] } }
                    >Unit Price</TableHeaderColumn>
                    <TableHeaderColumn dataField="actions" dataFormat={this.actionsFormatter}></TableHeaderColumn>
                </BootstrapTable>
            </div>
        )
    }
}