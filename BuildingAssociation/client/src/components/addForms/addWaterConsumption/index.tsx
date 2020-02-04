import * as React from 'react';
import { RouteComponentProps, Redirect } from 'react-router';
import { WaterConsumption } from '../../../models/WaterConsumption';
import moment from 'moment';

interface AddWaterConsumptionState {
    kitchenUnits?: number;
    bathroomUnits?: number;
    userId?: number;
    date: string;
    saved: boolean;
    consumptions: WaterConsumption[];
}

export default class AddWaterConsumption extends React.Component<RouteComponentProps<any>, AddWaterConsumptionState> {
    constructor(props: RouteComponentProps<any>) {
        super(props);

        this.state = {
            saved: false,
            kitchenUnits: undefined,
            bathroomUnits: undefined,
            date: moment.utc().toISOString(),
            consumptions: []
        }
    }

    componentDidMount(){
        const { id } = this.props.match.params;
        this.initData(id);
   }

   initData = async (id: number) => {
    if(sessionStorage.getItem('authToken') != null) {
        const consumptionsFromDb: WaterConsumption[] = await fetch(`/waterconsumptions`, {
            headers: {
                'Authorization': sessionStorage.getItem('authToken')
            } 
        } as RequestInit).then(response => {
            if(response.ok) {
            return response.json();
            }

            return undefined;
        });

        if(id){
            const item: WaterConsumption = await fetch(`/waterconsumptions/${id}`, {
                headers: {
                    'Authorization': sessionStorage.getItem('authToken')
                } 
            } as RequestInit).then(response => {
                if(response.ok) {
                return response.json();
                }
    
                return undefined;
            });

            if(item) {
                this.setState({
                    consumptions: consumptionsFromDb,
                    bathroomUnits: item.bathroomUnits,
                    kitchenUnits: item.kitchenUnits,
                    date: item.creationDate,
                    userId: item.userId,
                })
            }
        } else {
            this.setState({
                consumptions: consumptionsFromDb,
            })
        }
    }
}

    submit = (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();

        const item: WaterConsumption = {
            bathroomUnits: this.state.bathroomUnits as number,
            kitchenUnits: this.state.kitchenUnits as number,
            creationDate: moment.utc().toISOString(),
            id: this.props.match.params.id,
            mansionName: "",
            userName: "",
            userId: this.state.userId
        }

        fetch('/waterconsumptions', {
            method: 'POST',
            body: JSON.stringify(item),
            headers: {
                'Content-Type': 'application/json',
                'Authorization': sessionStorage.getItem('authToken')
            }
        } as RequestInit).then(result => { this.setState({saved: true})});
    }

    render() {
        if(sessionStorage.getItem('authToken') == null) {
            return <div>
                Nu esti logat!
            </div>
        }

        if(this.state.saved)
        {
           return <Redirect to="/waterconsumptions" /> 
        }
        
        const isAdmin: boolean = sessionStorage.getItem('isAdmin') === 'true';
        const consumptionsExist = this.state.consumptions && this.state.consumptions.length;
        const consumptionByUser = this.props.match.params.id && isAdmin 
            ? this.state.consumptions.filter(x => x.userId === this.state.userId)
            : undefined;
        
        const kitchenMin = consumptionsExist ? (consumptionByUser ? consumptionByUser[0].kitchenUnits : this.state.consumptions[0].kitchenUnits) : 0;
        const bathroomMin = consumptionsExist ? (consumptionByUser ? consumptionByUser[0].bathroomUnits : this.state.consumptions[0].bathroomUnits) : 0;

        const formTitle = `${this.props.match.params.id ? "Modify" : "Add" } water consumption`;

        return (
            <form className="container waterconsumptions-container" onSubmit={this.submit}>
                <h3>{formTitle}</h3>
                <h4>Last consumption: </h4>
                <h6>Kitchen: {kitchenMin}</h6>
                <h6>Bathroom: {bathroomMin}</h6>
                <div className="form-group">
                    <label>Kitchen units</label>
                    <input
                        type="number"
                        step="any"
                        min={kitchenMin}
                        onChange={(e) => this.setState({kitchenUnits: parseFloat(e.target.value)}) }
                        className="form-control"
                        required
                        defaultValue={this.state.kitchenUnits}
                    />
                </div>
                <div className="form-group">
                    <label>Bathroom units</label>
                    <input
                        type="number"
                        step="any"
                        min={bathroomMin}
                        onChange={(e) => this.setState({bathroomUnits: parseFloat(e.target.value)}) }
                        className="form-control"
                        defaultValue={this.state.bathroomUnits}
                    />
                </div>
                <button type="submit" className="btn btn-primary">Submit</button>
            </form>
        )
    }
}