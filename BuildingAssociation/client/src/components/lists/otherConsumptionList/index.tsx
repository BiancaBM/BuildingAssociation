import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import { BootstrapTable, TableHeaderColumn } from 'react-bootstrap-table';
import { Link } from 'react-router-dom';
import { OtherConsumption } from '../../../models/OtherConsumption';

interface OtherConsumptionListState {
    consumptions: OtherConsumption[];
    reload: boolean;
}

export default class OtherConsumptionList extends React.Component<RouteComponentProps<any>, OtherConsumptionListState> {
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

    componentDidUpdate(prevProps: any, prevState: OtherConsumptionListState) {
        if(prevState.reload !== this.state.reload && this.state.reload) {
            this.initData();
        }
    }

    initData = () => {
        if(sessionStorage.getItem('authToken') != null) {
            fetch(`/otherconsumptions`, {
                headers: {
                    'Authorization': sessionStorage.getItem('authToken')
                } 
            } as RequestInit).then(response => {
                if(response.ok) {
                return response.json();
                }

                return undefined;
            }).then((result: OtherConsumption[]) => {
                this.setState({ consumptions: result, reload: false })
            });
        }
    }
    
    enumFormatter = (cell: any, row: any, enumObject: any) => {
        return enumObject[cell];
    }

    getMansionsEnum = () => {
        const mansions: any = {};
        this.state.consumptions && this.state.consumptions.forEach(item => {
            if(!(`${item.mansionId}` in mansions)) {
                mansions[`${item.mansionId}`] = item.mansionName
            }
        });

        return mansions; 
    }

    deleteRow = (item: OtherConsumption) => {
        if(sessionStorage.getItem('authToken') != null) {
            fetch(`/otherconsumptions/${item.id}`, {
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

    actionsFormatter = (cell: any, row: OtherConsumption) => {
        return <>
            <Link to={`/addconsumption/${row.id}`} className="fas fa-edit"></Link>
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

        const mansionsType = this.getMansionsEnum();
        const calculationType = {
            '0': 'Number of members',
            '1': 'Indiviual Quota'
        }; 
        return (
            <div className="container consumptiontypelist-container">
                <Link to={'/addconsumption'} className="btn btn-info">Add consumption</Link>

                <BootstrapTable data={this.state.consumptions} containerClass="mt-3"
                    striped hover
                    search
                    exportCSV={ true }
                    version='4'
                    options={{
                        noDataText: 'No data!' ,
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
                    <TableHeaderColumn isKey hidden dataField='id'>ID</TableHeaderColumn>
                    <TableHeaderColumn dataField='name' filter={ { type: 'TextFilter' } } dataSort={true}>Name</TableHeaderColumn>
                    <TableHeaderColumn
                        dataField='mansionId'
                        dataSort={true} filterFormatted dataFormat={ this.enumFormatter }
                        formatExtraData={ mansionsType }
                        filter={ { type: 'SelectFilter', options: mansionsType } }
                    >Mansion Name</TableHeaderColumn>
                    <TableHeaderColumn
                        dataField='calculationType'
                        dataSort={true} filterFormatted dataFormat={ this.enumFormatter }
                        formatExtraData={ calculationType }
                        filter={ { type: 'SelectFilter', options: calculationType } }
                    >Type</TableHeaderColumn>
                    <TableHeaderColumn dataField='date' dataSort={true}>Date</TableHeaderColumn>
                    <TableHeaderColumn
                        dataField='price'
                        dataSort={true}
                        filter={ { type: 'NumberFilter', delay: 1000, numberComparators: [ '=', '>', '<=' ] } }
                    >Price</TableHeaderColumn>
                    <TableHeaderColumn dataField="actions" dataFormat={this.actionsFormatter}></TableHeaderColumn>
                </BootstrapTable>
            </div>
        )
    }
}