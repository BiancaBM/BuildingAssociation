import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import { BootstrapTable, TableHeaderColumn } from 'react-bootstrap-table';
import { BillGenerator } from '../../../models/BillGenerator';
import download from 'downloadjs';

interface GeneratedBillListState {
    bills: BillGenerator[];
    reload: boolean;
}

export default class GeneratedBillList extends React.Component<RouteComponentProps<any>, GeneratedBillListState> {
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

    componentDidUpdate(prevProps: any, prevState: GeneratedBillListState) {
        if(prevState.reload !== this.state.reload && this.state.reload) {
            this.initData();
        }
    }

    initData = () => {
        if(sessionStorage.getItem('authToken') != null) {
            fetch(`/billgenerator`, {
                headers: {
                    'Authorization': sessionStorage.getItem('authToken')
                } 
            } as RequestInit).then(response => {
                if(response.ok) {
                return response.json();
                }

                return undefined;
            }).then((result: BillGenerator[]) => {
                this.setState({ bills: result, reload: false })
            });
        }
    }

    deleteRow = (item: BillGenerator) => {
        if(sessionStorage.getItem('authToken') != null) {
            fetch(`/billgenerator/${item.id}`, {
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

    downloadCsv = (item: BillGenerator) => {
        if(sessionStorage.getItem('authToken') != null) {
            fetch(`/billgenerator/${item.id}`, {
                    method: 'GET',
                    headers: {
                        'Authorization': sessionStorage.getItem('authToken')
                    },
                } as RequestInit).then((response : Response) : any => {
                if (response.ok) {
                    let contentDisposition =response.headers.get('content-disposition');
                    const fileName: string = contentDisposition?.split('filename=').reverse()[0] ?? "raport.csv";
                    
                    response.blob().then(data => download(data, fileName));
                    return;
                }
            });
        }
    }

    actionsFormatter = (cell: any, row: BillGenerator) => {
        const isAdmin = sessionStorage.getItem("isAdmin") === "true";
        return <>
            {isAdmin &&
            <i
                className="fas fa-trash-alt ml-3"
                onClick={() => this.deleteRow(row)}></i>}
            <i
                className="fas fa-download ml-3"
                onClick={() => this.downloadCsv(row) }></i>
        </>;
    }

    enumFormatter = (cell: any, row: any, enumObject: any) => {
        return enumObject[cell];
    }

    getMansionsEnum = () => {
        const mansions: any = {};

        if(this.state.bills){
            this.state.bills.forEach((item: BillGenerator) => {
                if(!(`${item.mansionName}` in mansions)) {
                    mansions[`${item.mansionId}`] = item.mansionName
                }
            });
        }

        return mansions; 
    }
    
    render() {
        if(sessionStorage.getItem('authToken') == null) {
            return <div>
                Nu esti logat!
            </div>
        }

        const mansionsType = this.getMansionsEnum();
        const isAdmin: boolean = sessionStorage.getItem('isAdmin') === 'true';

        return (
            <div className="container generatedbill-container">
                <BootstrapTable 
                    search
                    data={this.state.bills}
                    containerClass="mt-3"
                    striped hover
                    version='4'
                    options={{
                        noDataText: 'No data!' ,
                        defaultSortName: 'date',
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
                    <TableHeaderColumn dataField='date'>Date</TableHeaderColumn>
                    <TableHeaderColumn
                        hidden={!isAdmin}
                        dataField='mansionId'
                        dataSort={true} filterFormatted dataFormat={ this.enumFormatter }
                        formatExtraData={ mansionsType }
                        filter={ { type: 'SelectFilter', options: mansionsType } }
                    >Mansion Name</TableHeaderColumn>
                    <TableHeaderColumn dataField="actions" dataFormat={this.actionsFormatter} export={false}></TableHeaderColumn>
                </BootstrapTable>
            </div>
        )
    }
}