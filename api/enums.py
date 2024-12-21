from enum import StrEnum, unique


@unique
class ESBRequestType(StrEnum):
    SEND_EMAIL_MSG = "send_email_msg"
    SEND_TG_MSG = "send_tg_msg"


@unique
class MessageType(StrEnum):
    EMAIL = "email"
    TG = "tg"


@unique
class ESBRequestState(StrEnum):
    SENDING = "sending"  # запрос отправляется
    SENDING_ERROR = "sending_error"  # ошибка отправки
    SENT = "sent"  # запрос отправлен
    RESPONSE_SUCCESS = "response_success"  # успешный ответ
    RESPONSE_ERROR = "response_error"  # ошибка в ответе сервиса
    CANCELLED = "cancelled"  # запрос отменен вручную
