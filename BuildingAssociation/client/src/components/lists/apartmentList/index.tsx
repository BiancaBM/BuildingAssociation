import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import { BootstrapTable, TableHeaderColumn } from 'react-bootstrap-table';
import { Link } from 'react-router-dom';
import { Apartment } from '../../../models/Apartment';

interface ApartmentListState {
    apartments: Apartment[];
    reload: boolean;
}

export default class ApartmentList extends React.Component<RouteComponentProps<any>, ApartmentListState> {
    constructor(props: RouteComponentProps<any>) {
        super(props);

        this.state = {
            apartments: [],
            reload: false
        }
    }

    componentDidMount() {
        this.initData();
    }

    componentDidUpdate(prevProps: any, prevState: ApartmentListState) {
        if(prevState.reload !== this.state.reload && this.state.reload) {
            this.initData();
        }
    }

    initData = () => {
        if(sessionStorage.getItem('authToken') != null) {
            fetch(`/apartments`, {
                headers: {
                    'Authorization': sessionStorage.getItem('authToken')
                } 
            } as RequestInit).then(response => {
                if(response.ok) {
                return response.json();
                }

                return undefined;
            }).then((result: Apartment[]) => {
                this.setState({ apartments: result, reload: false })
            });
        }
    }
    
    enumFormatter = (cell: any, row: any, enumObject: any) => {
        return enumObject[cell];
    }

    getMansionsEnum = () => {
        const mansions: any = {};
        this.state.apartments && this.state.apartments.forEach(item => {
            if(!(`${item.mansionId}` in mansions)) {
                mansions[`${item.mansionId}`] = item.mansionName
            }
        });

        return mansions; 
    }

    deleteRow = (item: Apartment) => {
        if(sessionStorage.getItem('authToken') != null) {
            fetch(`/apartments/${item.apartmentId}`, {
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

    actionsFormatter = (cell: any, row: Apartment) => {
        return <>
            <Link to={`/addapartment/${row.apartmentId}`} className="fas fa-edit"></Link>
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

        return (
            <div className="container consumptiontypelist-container">
                <Link to={'/addapartment'} className="btn btn-info">Add apartment</Link>

                <BootstrapTable data={this.state.apartments} containerClass="mt-3"
                    striped hover
                    exportCSV
                    search
                    version='4'
                    options={{
                        noDataText: 'No data!' ,
                        defaultSortName: 'number',
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
                    <TableHeaderColumn isKey hidden dataField='apartmentId'>ID</TableHeaderColumn>
                    <TableHeaderColumn hidden dataField='userId'>userId</TableHeaderColumn>
                    <TableHeaderColumn
                        dataField='mansionId'
                        dataSort={true} filterFormatted dataFormat={ this.enumFormatter }
                        formatExtraData={ mansionsType }
                        filter={ { type: 'SelectFilter', options: mansionsType } }
                    >Mansion Name</TableHeaderColumn>
                     <TableHeaderColumn
                        dataField='number'
                        dataSort={true}
                        filter={ { type: 'NumberFilter', delay: 1000, numberComparators: [ '=', '>', '<=' ] } }
                    >Number</TableHeaderColumn>
                    <TableHeaderColumn
                        dataField='surface'
                        dataSort={true}
                        filter={ { type: 'NumberFilter', delay: 1000, numberComparators: [ '=', '>', '<=' ] } }
                    >Surface</TableHeaderColumn>
                    <TableHeaderColumn
                        dataField='floor'
                        dataSort={true}
                        filter={ { type: 'NumberFilter', delay: 1000, numberComparators: [ '=', '>', '<=' ] } }
                    >Floor</TableHeaderColumn>
                    <TableHeaderColumn
                        dataField='individualQuota'
                        dataSort={true}
                        filter={ { type: 'NumberFilter', delay: 1000, numberComparators: [ '=', '>', '<=' ] } }
                    >Individual quota</TableHeaderColumn>
                    <TableHeaderColumn dataField='userName' filter={ { type: 'TextFilter' } } dataSort={true}>User Name</TableHeaderColumn>
                    <TableHeaderColumn dataField="actions" dataFormat={this.actionsFormatter}></TableHeaderColumn>
                </BootstrapTable>
            </div>
        )
    }
}