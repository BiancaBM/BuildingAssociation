//     This code was generated by a Reinforced.Typings tool. 
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.

import * as WaterConsumption from './WaterConsumption';
import * as Apartment from './Apartment';

export interface User
{
	userId?: number;
	name: string;
	isAdmin: boolean;
	email: string;
	membersCount: number;
	waterConsumptions: WaterConsumption.WaterConsumption[];
	apartments: Apartment.Apartment[];
}
