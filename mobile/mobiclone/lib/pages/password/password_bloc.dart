import 'package:bloc/bloc.dart';
import 'package:mobiclone/core/sign_up/password_repository.dart';
import 'package:mobiclone/pages/password/password_event.dart';
import 'package:mobiclone/pages/password/password_state.dart';

class PasswordBloc extends Bloc<PasswordEvent, PasswordState> {
  final PasswordRepository _passwordRepository;

  PasswordBloc(this._passwordRepository);

  @override
  PasswordState get initialState => InitiatedPasswordState();

  @override
  Stream<PasswordState> mapEventToState(PasswordEvent event) async* {
    if (event is PasswordEventSubmit) {
      yield SubmittedPasswordState(event.value);

      if (event.value.isEmpty) {
        yield RequiredPasswordState(
          event.value,
          'The password must be informed.',
        );
      } else {
        final password = await _passwordRepository.addPassword(event.value);
        yield ValidatedPasswordState(password);
      }
    }
  }
}
