import Address from "./Address";

export default interface Owner {
  Id: number;
  Name: string;
  Document: string;
  Email: string;
  Address: Address;
  Status: number;
}
