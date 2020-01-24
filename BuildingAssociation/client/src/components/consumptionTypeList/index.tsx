import * as React from 'react';
import './style.css';
import { RouteComponentProps } from 'react-router';
import { BootstrapTable, TableHeaderColumn } from 'react-bootstrap-table';
import { Link } from 'react-router-dom';
import { ConsumptionType } from '../../models/ConsumptionType';

interface ConsumptionTypeListState {
    consumptions: ConsumptionType[];
}

export default class ConsumptionTypeList extends React.Component<RouteComponentProps<any>, ConsumptionTypeListState> {
    constructor(props: RouteComponentProps<any>) {
        super(props);

        this.state = {
            consumptions: [],
        }
    }

    componentDidMount() {
        this.initData();
    }

    initData = () => {
        if(sessionStorage.getItem('authToken') != null) {
            fetch(`/consumptiontype`, {
                headers: {
                    'Authorization': sessionStorage.getItem('authToken')
                } 
            } as RequestInit).then(response => {
                if(response.ok) {
                return response.json();
                }

                return undefined;
            }).then((result: ConsumptionType[]) => {
                this.setState({ consumptions: result })
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
                <Link to={'/addconsumptiontype'} className="btn btn-info">Add consumption type</Link>

                <BootstrapTable data={this.state.consumptions} containerClass="mt-3"
                    striped hover
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
                </BootstrapTable>
            </div>
        )
    }
}