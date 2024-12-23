# Разработка в контейнере

1. Запустите в корне проекта

```bash
docker compose watch api
```

# Локальная разработка

1. [Установить uv](https://docs.astral.sh/uv/getting-started/installation/)
2. Создать окружение, находясь в [/api](), и установить pre-commit хуки

```bash
uv venv
uv sync --all-groups
uv run pre-commit install
```

3. Запуск линтера

```bash
uv run ruff check src
```

и форматтера

```bash
uv run ruff format src
```