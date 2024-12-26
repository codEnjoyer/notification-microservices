from pydantic_extra_types.phone_numbers import PhoneNumber

from enums import MessageType
from src.schemas.message import BrokerMessage, MessageSchema


class TgMessageIn(MessageSchema):
    address: PhoneNumber


class BrokerTgMessage(BrokerMessage, TgMessageIn):
    type: MessageType = MessageType.TG
