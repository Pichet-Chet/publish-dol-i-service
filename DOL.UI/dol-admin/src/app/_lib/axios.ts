import axios, { AxiosRequestTransformer } from "axios";
import { getSession } from "next-auth/react";
import dayjs from "dayjs";
import utc from "dayjs/plugin/utc";
import timezone from "dayjs/plugin/timezone";
dayjs.extend(utc);
dayjs.extend(timezone);
dayjs.tz.setDefault("Asia/Bangkok");

export const API_APP_URL = process.env.NEXT_PUBLIC_APP_HOST;
// export const API_IDENTITY_URL = process.env.NEXT_PUBLIC_IDENTITY_HOST;

const defaultOptions = {};

const replacer = function (this: any, key: string, value: any) {
  if (this[key] instanceof dayjs) {
    return dayjs(this[key]).format("YYYY-MM-DD HH:mm:ss");
  }
  return value;
};

const instance = axios
  .create
  // {
  //   transformRequest: [(data, headers) => JSON.parse(JSON.stringify(data, replacer) || "{}"), ...(axios.defaults.transformRequest as AxiosRequestTransformer[])]
  // }
  ();

instance.interceptors.request.use(async (request) => {
  const currentSession = await getSession();
  request.headers.set(
    "Authorization",
    `Bearer ${currentSession?.user.access_token ?? ""}`
  );
  request.headers.set("Access-Control-Allow-Origin", "*");
  request.headers.set(
    "Access-Control-Allow-Methods",
    "GET, POST, PUT, DELETE, OPTIONS, FETCH"
  );
  request.headers.set(
    "Access-Control-Allow-Headers",
    "Content-Type, Authorization"
  );
  return request;
});

instance.interceptors.response.use(
  async (response) => {
    return response;
  },
  async (error) => {
    return Promise.reject(error);
  }
);

export default instance;
