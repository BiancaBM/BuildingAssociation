import * as _ from 'lodash';

const portServer = 3073;
const portSpa = 8080;
export const serverUrl = `http://localhost:${portServer}`;
export const localUrl = `http://localhost:${portSpa}`;
export const apiUrl = `${serverUrl}/api`;

export const formatUrl = (urlStr: string, params: any) => urlStr + '?' + 
      new URLSearchParams(_.pickBy(params, _.negate(_.isNil))).toString();