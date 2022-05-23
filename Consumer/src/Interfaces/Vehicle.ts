import Owner from "./Owner";
import Brand from "./Brand";

export default interface Vehicle {
  Id: number;
  Owner: Owner;
  OwnerId: number;
  Renavam: string;
  Brand: Brand;
  BrandId: number;
  Model: string;
  YearCreation: number;
  YearModel: number;
  Quilometers: number;
  Value: number;
  Status: number;
}
