from fastapi import APIRouter

from enums import ESBRequestType
from src.broker.producer import notifications_publisher
from src.dependencies import ESBRequestServiceDep
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
    esb_request_service: ESBRequestServiceDep,
) -> dict:
    broker_msg = BrokerEmailMessage.from_message_in(payload)
    request = await esb_request_service.save_notification_request(
        payload,
        ESBRequestType.SEND_EMAIL_MSG,
    )
    try:
        await notifications_publisher.publish(message=broker_msg)
    except Exception:  # TODO: Заменить на конкретную ошибку во время отправки
        await esb_request_service.mark_request_failed(request.id)
    await esb_request_service.mark_request_sent(request.id)
    return payload


@router.post(
    "/tg",
)
async def send_tg_message(
    payload: TgMessageIn,
    esb_request_service: ESBRequestServiceDep,
) -> dict:
    broker_msg = BrokerTgMessage.from_message_in(payload)
    request = await esb_request_service.save_notification_request(
        payload,
        ESBRequestType.SEND_TG_MSG,
    )
    try:
        await notifications_publisher.publish(message=broker_msg)
    except Exception:  # TODO: Заменить на конкретную ошибку во время отправки
        await esb_request_service.mark_request_failed(request.id)
    await esb_request_service.mark_request_sent(request.id)
    return payload
