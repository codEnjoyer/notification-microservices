from settings import broker_settings
from src.broker.broker import broker

notifications_publisher = broker.publisher(broker_settings.notifications_topic)
