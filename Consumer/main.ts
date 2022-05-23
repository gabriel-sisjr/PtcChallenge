import "dotenv/config";
import { Channel } from "amqplib";
import createRabbitmqConnection from "./src/Settings/RabbitmqSettings";
import createTransporter from "./src/Settings/EmailSettings";
import { sendEmail } from "./src/Services/EmailService";

(async () => {
  const channel: Channel = await createRabbitmqConnection();
  const transporter = createTransporter();

  await sendEmail(channel, transporter);
})();
