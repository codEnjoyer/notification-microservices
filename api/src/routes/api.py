from fastapi import APIRouter

from src.broker.producer import notifications_publisher
from src.schemas.email import BrokerEmailMessage, EmailMessageIn
from src.schemas.tg import BrokerTgMessage, TgMessageIn

router = APIRouter(
    prefix="/api",
)


# idempotency key?
@router.post(
    "/email",
)
async def send_email_message(
    payload: EmailMessageIn,
) -> dict:
    broker_msg = BrokerEmailMessage.from_message_in(payload)
    await notifications_publisher.publish(message=broker_msg)
    return payload


@router.post(
    "/tg",
)
async def send_tg_message(
    payload: TgMessageIn,
) -> dict:
    broker_msg = BrokerTgMessage.from_message_in(payload)
    await notifications_publisher.publish(message=broker_msg)
    return payload
