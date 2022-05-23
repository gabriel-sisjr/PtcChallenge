import { EmailSettings } from "../Interfaces/Auxs/Email";
import nodemailer from "nodemailer";

const emailSettings: EmailSettings = {
  host: process.env.EMAIL_HOST || "smtp.mailtrap.io",
  port: Number(process.env.EMAIL_PORT) || 2525,
  auth: {
    user: process.env.EMAIL_HOST_USER || "f7b485f059ea8e",
    pass: process.env.EMAIL_HOST_PASSWORD || "833ef85cf8a6df",
  },
};

const createTransporter = () => nodemailer.createTransport(emailSettings);

export default createTransporter;
