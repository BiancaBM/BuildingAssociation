import * as React from 'react';
import { RouteComponentProps, Redirect } from 'react-router';
import { WaterConsumption } from '../../../models/WaterConsumption';
import moment from 'moment';

interface AddWaterConsumptionState {
    hotWaterUnits?: number;
    coldWaterUnits?: number;
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
            hotWaterUnits: undefined,
            coldWaterUnits: undefined,
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
                    coldWaterUnits: item.coldWaterUnits,
                    hotWaterUnits: item.hotWaterUnits,
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
            coldWaterUnits: this.state.coldWaterUnits as number,
            hotWaterUnits: this.state.hotWaterUnits as number,
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
        
        const coldMin = consumptionsExist ? (consumptionByUser ? consumptionByUser[0].coldWaterUnits : this.state.consumptions[0].coldWaterUnits) : 0;
        const hotMin = consumptionsExist ? (consumptionByUser ? consumptionByUser[0].hotWaterUnits : this.state.consumptions[0].hotWaterUnits) : 0;

        return (
            <form className="container waterconsumptions-container" onSubmit={this.submit}>
                <h3>Add water consumption</h3>
                <h4>Last consumption: </h4>
                <h6>Cold: {coldMin}</h6>
                <h6>Hot: {hotMin}</h6>
                <div className="form-group">
                    <label>Cold water units</label>
                    <input
                        type="number"
                        step="any"
                        min={coldMin}
                        onChange={(e) => this.setState({coldWaterUnits: parseFloat(e.target.value)}) }
                        className="form-control"
                        required
                        value={this.state.coldWaterUnits}
                    />
                </div>
                <div className="form-group">
                    <label>Hot water units</label>
                    <input
                        type="number"
                        step="any"
                        min={hotMin}
                        onChange={(e) => this.setState({hotWaterUnits: parseFloat(e.target.value)}) }
                        className="form-control"
                        value={this.state.hotWaterUnits}
                    />
                </div>
                <button type="submit" className="btn btn-primary">Submit</button>
            </form>
        )
    }
}