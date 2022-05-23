import { Channel } from "amqplib";
import nodemailer from "nodemailer";
import SMTPTransport from "nodemailer/lib/smtp-transport";
import { EmailInfo } from "../Interfaces/Auxs/Email";
import { QueueName } from "../Interfaces/Enums/QueueName";
import Vehicle from "../Interfaces/Vehicle";

let message: EmailInfo = {
  from: process.env.EMAIL_FROM || "from-example@email.com",
  to: "",
  subject: "Vehicle Inserted",
  text: "Vehicle inserted successfully",
};

const sendEmail = async (
  channel: Channel,
  transporter: nodemailer.Transporter<SMTPTransport.SentMessageInfo>
) => {
  await channel.assertQueue(QueueName.EMAIL);
  await channel.consume(QueueName.EMAIL, (msg) => {
    let vehicle = JSON.parse(msg!.content.toString()) as Vehicle;
    message.to = vehicle.Owner.Email;

    transporter
      .sendMail(message)
      .then((info) => console.log(info))
      .catch((err) => console.log(err));

    channel.ack(msg!);
  });
};

export { sendEmail };
