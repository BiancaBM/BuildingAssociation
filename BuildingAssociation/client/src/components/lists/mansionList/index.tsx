import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import { Mansion } from '../../../models/Mansion';
import { BootstrapTable, TableHeaderColumn } from 'react-bootstrap-table';
import { Link } from 'react-router-dom';
import moment from 'moment';
import { BillGenerator } from '../../../models/BillGenerator';

interface MansionListState {
    mansions: Mansion[];
    reload: boolean;
}

export default class MansionList extends React.Component<RouteComponentProps<any>, MansionListState> {
    constructor(props: RouteComponentProps<any>) {
        super(props);

        this.state = {
            mansions: [],
            reload: false
        }
    }

    componentDidMount() {
        this.initData();
    }

    componentDidUpdate(prevProps: any, prevState: MansionListState) {
        if(prevState.reload !== this.state.reload && this.state.reload) {
            this.initData();
        }
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
                this.setState({ mansions: result, reload: false })
            });
        }
    }

    deleteRow = (mansion: Mansion) => {
        if(sessionStorage.getItem('authToken') != null) {
            fetch(`/mansions/${mansion.id}`, {
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

    generate = (mansion: Mansion) => {
        if(sessionStorage.getItem('authToken') != null) {
            var params: BillGenerator = {
                mansionId: mansion.id as number,
                csv: "",
                date: "",
                mansionName: ""
            }

            fetch('/billGenerator', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json',
                        'Authorization': sessionStorage.getItem('authToken')
                    },
                    body: JSON.stringify(params)
                } as RequestInit).then(response => {
                if (response.ok) {
                    alert('Bill generated! Check on "Bill genereted" section!')
                    return;
                }
                return response.json();
            }).then((error) => {
                if(error) {
                    alert(error);
                }
            });
        }
    }

    actionsFormatter = (cell: any, row: Mansion) => {
        let canNotDeleteMessage: string = "You can not delete because you have:\n";
        let canNotDelete = false;
        if(row.users.length > 0) {
            canNotDeleteMessage+= 'Users linked\n';
            canNotDelete = true;
        }

        if(row.bills.length > 0) {
            canNotDeleteMessage+= 'Provider bills linked\n';
            canNotDelete = true;
        }

        if(row.consumptions.length > 0) {
            canNotDeleteMessage+= 'Consumptions linked\n';
            canNotDelete = true;
        }

        return <>
            <Link to={`/addmansion/${row.id}`} className="fas fa-edit"></Link>
            <i
                className={`fas fa-trash-alt ml-3 ${canNotDelete && 'disabled'}`}
                title={canNotDelete ? canNotDeleteMessage : ""}
                onClick={() => !canNotDelete ? this.deleteRow(row) : undefined}></i>
            <button
                className="btn btn-primary ml-3"
                onClick={() => this.generate(row)}>{`Generate for ${moment().subtract(1, "month").format('MMMM')} ${moment().subtract(1, "year").format('YYYY')}`}</button>
        </>;
    }

    editBtnFormatter = (cell: any, row: Mansion) => {
        return ;
    }
    
    render() {
        if(sessionStorage.getItem('authToken') == null) {
            return <div>
                Nu esti logat!
            </div>
        }

        return (
            <div className="container mansonlist-container">
                <Link to={'/addmansion'} className="btn btn-info">Add mansion</Link>

                <BootstrapTable 
                    search
                    exportCSV
                    data={this.state.mansions}
                    containerClass="mt-3"
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
                    <TableHeaderColumn dataField="actions" dataFormat={this.actionsFormatter} export={false}></TableHeaderColumn>
                </BootstrapTable>
            </div>
        )
    }
}