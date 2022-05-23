import client, { Channel, Connection } from "amqplib";
import RabbitmqSettings from "../Interfaces/Auxs/Rabbitmq";

const rabbitmqSettings: RabbitmqSettings = {
  hostname: process.env.RABBITMQ_HOST || "rabbitmq",
  port: Number(process.env.RABBITMQ_PORT) || 5672,
  username: process.env.RABBITMQ_USER || "admin",
  password: process.env.RABBITMQ_PASS || "admin",
};

const createRabbitmqConnection = async (): Promise<Channel> => {
  const connection: Connection = await client.connect(rabbitmqSettings);
  const channel: Channel = await connection.createChannel();
  return channel;
};

export default createRabbitmqConnection;
