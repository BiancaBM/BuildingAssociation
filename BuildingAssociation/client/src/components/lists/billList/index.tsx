import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import { ProviderBill } from '../../../models/ProviderBill';
import { BootstrapTable, TableHeaderColumn } from 'react-bootstrap-table';
import { Link } from 'react-router-dom';

interface BillListState {
    bills: ProviderBill[];
    reload: boolean;
}

export default class BillList extends React.Component<RouteComponentProps<any>, BillListState> {
    constructor(props: RouteComponentProps<any>) {
        super(props);

        this.state = {
            bills: [],
            reload: false
        }
    }

    componentDidMount() {
        this.initData();
    }

    componentDidUpdate(prevProps: any, prevState: BillListState) {
        if(prevState.reload !== this.state.reload && this.state.reload) {
            this.initData();
        }
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
                this.setState({ bills: result, reload: false })
            });
        }
    }

    enumFormatter = (cell: any, row: any, enumObject: any) => {
        return enumObject[cell];
    }

    deleteRow = (item: ProviderBill) => {
        if(sessionStorage.getItem('authToken') != null) {
            fetch(`/providerbills/${item.billId}`, {
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

    actionsFormatter = (cell: any, row: ProviderBill) => {
        return <>
            <Link to={`/addbill/${row.billId}`} className="fas fa-edit"></Link>
            <i
                className="fas fa-trash-alt ml-3"
                onClick={() => this.deleteRow(row)}></i>
        </>;
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
                    exportCSV
                    search
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
                    <TableHeaderColumn dataField="actions" dataFormat={this.actionsFormatter}></TableHeaderColumn>
                </BootstrapTable>
            </div>
        )
    }
}