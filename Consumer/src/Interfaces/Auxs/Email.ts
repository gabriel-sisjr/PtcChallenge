export interface EmailInfo {
  from: string;
  to: string;
  subject: string;
  text: string;
}

export interface EmailSettings {
  host: string;
  port: number;
  secure?: boolean;
  auth: {
    user: string;
    pass: string;
  };
  tls?: {
    rejectUnauthorized: boolean;
    ciphers: string;
  };
}
