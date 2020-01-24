import * as React from 'react';
import './style.css';
import { RouteComponentProps } from 'react-router';
import { ProviderBill } from '../../models/ProviderBill';
import { BootstrapTable, TableHeaderColumn } from 'react-bootstrap-table';
import { Link } from 'react-router-dom';

interface BillListState {
    bills: ProviderBill[];
}

export default class BillList extends React.Component<RouteComponentProps<any>, BillListState> {
    constructor(props: RouteComponentProps<any>) {
        super(props);

        this.state = {
            bills: [],
        }
    }

    componentDidMount() {
        this.initData();
    }

    initData = () => {
        if(sessionStorage.getItem('authToken') != null) {
            fetch(`/providerbills`, {
                headers: {
                    'Authorization': sessionStorage.getItem('authToken')
                } 
            } as RequestInit).then(response => {
                if(response.ok) {
                return response.json();
                }

                return undefined;
            }).then((result: ProviderBill[]) => {
                this.setState({ bills: result })
            });
        }
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

        const qualityType = {
            'true': 'Yes',
            'false': 'No'
        };
          
        return (
            <div className="container billlist-container">
                <Link to={'/addbill'} className="btn btn-info">Add bill</Link>

                <BootstrapTable data={this.state.bills} containerClass="mt-3"
                    striped hover
                    version='4'
                    options={{
                        noDataText: 'You paid all the bills! Congrats!' ,
                        defaultSortName: 'providerName',
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
                    <TableHeaderColumn isKey hidden dataField='billId'>Product ID</TableHeaderColumn>
                    <TableHeaderColumn dataField='providerId' hidden>Provider ID</TableHeaderColumn>
                    <TableHeaderColumn dataField='mansionName' filter={ { type: 'TextFilter' } } dataSort={true}>Mansion Name</TableHeaderColumn>
                    <TableHeaderColumn dataField='providerName' filter={ { type: 'TextFilter' } } dataSort={true}>Provider Name</TableHeaderColumn>
                    <TableHeaderColumn dataField='units' dataSort={true}>Units</TableHeaderColumn>
                    <TableHeaderColumn dataField='other' dataSort={true}>Other</TableHeaderColumn>
                    <TableHeaderColumn
                        dataField='paid'
                        dataSort={true} filterFormatted dataFormat={ this.enumFormatter }
                        formatExtraData={ qualityType }
                        filter={ { type: 'SelectFilter', options: qualityType, condition: 'eq' } }
                    >Paid</TableHeaderColumn>
                    <TableHeaderColumn
                        dataField='totalPrice'
                        dataSort={true}
                        filter={ { type: 'NumberFilter', delay: 1000, numberComparators: [ '=', '>', '<=' ] } }
                    >Total Price</TableHeaderColumn>
                    <TableHeaderColumn dataField='date' dataSort={true}>Date</TableHeaderColumn>
                    <TableHeaderColumn dataField='dueDate' dataSort={true}>Due Date</TableHeaderColumn>
                </BootstrapTable>
            </div>
        )
    }
}