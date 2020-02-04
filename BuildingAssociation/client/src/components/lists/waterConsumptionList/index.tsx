import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import { BootstrapTable, TableHeaderColumn } from 'react-bootstrap-table';
import { Link } from 'react-router-dom';
import { WaterConsumption } from '../../../models/WaterConsumption';

interface WaterConsumptionListState {
    consumptions: WaterConsumption[];
    reload: boolean;
}

export default class WaterConsumptionList extends React.Component<RouteComponentProps<any>, WaterConsumptionListState> {
    constructor(props: RouteComponentProps<any>) {
        super(props);

        this.state = {
            consumptions: [],
            reload: false
        }
    }

    componentDidMount() {
        this.initData();
    }

    componentDidUpdate(prevProps: any, prevState: WaterConsumptionListState) {
        if(prevState.reload !== this.state.reload && this.state.reload) {
            this.initData();
        }
    }
    
    initData = () => {
        if(sessionStorage.getItem('authToken') != null) {
            fetch(`waterconsumptions`, {
                headers: {
                    'Authorization': sessionStorage.getItem('authToken')
                } 
            } as RequestInit).then(response => {
                if(response.ok) {
                return response.json();
                }

                return undefined;
            }).then((result: WaterConsumption[]) => {
                this.setState({ consumptions: result, reload: false })
            });
        }
    }

    deleteRow = (item: WaterConsumption) => {
        if(sessionStorage.getItem('authToken') != null) {
            fetch(`/waterconsumptions/${item.id}`, {
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

    actionsFormatter = (cell: any, row: WaterConsumption) => {
        return <>
            <Link to={`/addwaterconsumption/${row.id}`} className="fas fa-edit"></Link>
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

        const isAdmin: boolean = sessionStorage.getItem('isAdmin') === 'true';

        return (
            <div className="container waterconsumption-container">
                {!isAdmin && <Link to={'/addwaterconsumption'} className="btn btn-info">Add water consumption</Link>}

                <BootstrapTable data={this.state.consumptions} containerClass="mt-3"
                    striped hover
                    version='4'
                    search
                    exportCSV
                    options={{
                        noDataText: 'No data!' ,
                        defaultSortName: 'creationDate',
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
                    <TableHeaderColumn isKey hidden dataField='id'>ID</TableHeaderColumn>
                    <TableHeaderColumn hidden={!isAdmin} dataField='userName' filter={ { type: 'TextFilter' } } dataSort={true}>User</TableHeaderColumn>
                    <TableHeaderColumn hidden={!isAdmin} dataField='mansionName' filter={ { type: 'TextFilter' } } dataSort={true}>Mansion</TableHeaderColumn>
                    <TableHeaderColumn dataField='kitchenUnits' dataSort={true}>Kitchen units</TableHeaderColumn>
                    <TableHeaderColumn dataField='bathroomUnits' dataSort={true}>Bathroom units</TableHeaderColumn>
                    <TableHeaderColumn dataField='creationDate' dataSort={true}>Date</TableHeaderColumn>
                    <TableHeaderColumn dataField="actions" dataFormat={this.actionsFormatter} hidden={!isAdmin} export={false}></TableHeaderColumn>
                </BootstrapTable>
            </div>
        )
    }
}