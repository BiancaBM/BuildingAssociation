//     This code was generated by a Reinforced.Typings tool. 
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.

import * as CalculationType from './CalculationType';

export interface OtherConsumption
{
	id?: number;
	name: string;
	calculationType: CalculationType.CalculationType;
	date: string;
	price: number;
	mansionId?: number;
	mansionName: string;
}