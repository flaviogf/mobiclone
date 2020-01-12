import 'package:bloc/bloc.dart';
import 'package:mobiclone/pages/password/password_event.dart';
import 'package:mobiclone/pages/password/password_state.dart';

class PasswordBloc extends Bloc<PasswordEvent, PasswordState> {
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
        yield ValidatedPasswordState(event.value);
      }
    }
  }
}
