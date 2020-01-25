import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import { BootstrapTable, TableHeaderColumn } from 'react-bootstrap-table';
import { Link } from 'react-router-dom';
import { User } from '../../../models/User';

interface UserListState {
    users: User[];
    reload: boolean;
}

export default class UserList extends React.Component<RouteComponentProps<any>, UserListState> {
    constructor(props: RouteComponentProps<any>) {
        super(props);

        this.state = {
            users: [],
            reload: false
        }
    }

    componentDidMount() {
        this.initData();
    }

    componentDidUpdate(prevProps: any, prevState: UserListState) {
        if(prevState.reload !== this.state.reload && this.state.reload) {
            this.initData();
        }
    }

    initData = () => {
        if(sessionStorage.getItem('authToken') != null) {
            fetch(`/users`, {
                headers: {
                    'Authorization': sessionStorage.getItem('authToken')
                } 
            } as RequestInit).then(response => {
                if(response.ok) {
                return response.json();
                }

                return undefined;
            }).then((result: User[]) => {
                this.setState({ users: result, reload: false })
            });
        }
    }

    enumFormatter = (cell: any, row: any, enumObject: any) => {
        return enumObject[cell];
    }

    getMansionsEnum = () => {
        const mansions: any = {};

        if(this.state.users){
            this.state.users.forEach(user => {
                if(!(`${user.mansionId}` in mansions)) {
                    mansions[`${user.mansionId}`] = user.mansionName
                }
            });
        }

        return mansions; 
    }

    deleteRow = (item: User) => {
        if(sessionStorage.getItem('authToken') != null) {
            fetch(`/users/${item.userId}`, {
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

    actionsFormatter = (cell: any, row: User) => {
        let canNotDeleteMessage: string = "You can not delete because:\n";
        let canNotDelete = false;
        if(row.waterConsumptions.length > 0) {
            canNotDeleteMessage+= 'Exist water consumptions linked\n';
            canNotDelete = true;
        }

        if(row.apartments.length > 0) {
            canNotDeleteMessage+= 'Exist apartments linked\n';
            canNotDelete = true;
        }

        if(row.isAdmin) {
            canNotDeleteMessage+= 'Is the admin!\n';
            canNotDelete = true;
        }

        return <>
            <Link to={`/adduser/${row.userId}`} className="fas fa-edit"></Link>
            <i
                className={`fas fa-trash-alt ml-3 ${canNotDelete && 'disabled'}`}
                title={canNotDelete ? canNotDeleteMessage : ""}
                onClick={() => !canNotDelete ? this.deleteRow(row) : undefined}></i>
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
            <div className="container userlist-container">
                <Link to={'/adduser'} className="btn btn-info">Add user</Link>

                <BootstrapTable data={this.state.users} containerClass="mt-3"
                    striped hover
                    exportCSV
                    search
                    version='4'
                    options={{
                        noDataText: 'No users in your application!' ,
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
                    <TableHeaderColumn isKey hidden dataField='userId'>User ID</TableHeaderColumn>
                    <TableHeaderColumn dataField='name' filter={ { type: 'TextFilter' } } dataSort={true}>Name</TableHeaderColumn>
                    <TableHeaderColumn dataField='email' filter={ { type: 'TextFilter' } } dataSort={true}>Email</TableHeaderColumn>
                    <TableHeaderColumn
                        dataField='mansionId'
                        dataSort={true} filterFormatted dataFormat={ this.enumFormatter }
                        formatExtraData={ mansionsType }
                        filter={ { type: 'SelectFilter', options: mansionsType } }
                    >Mansion Name</TableHeaderColumn>
                    <TableHeaderColumn dataField='membersCount' dataSort={true}>Members Count</TableHeaderColumn>
                    <TableHeaderColumn dataField="actions" dataFormat={this.actionsFormatter}></TableHeaderColumn>
                </BootstrapTable>
            </div>
        )
    }
}