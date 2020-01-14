import 'package:bloc/bloc.dart';
import 'package:mobiclone/data/user_repository.dart';
import 'package:mobiclone/pages/name/name_event.dart';
import 'package:mobiclone/pages/name/name_state.dart';

class NameBloc extends Bloc<NameEvent, NameState> {
  final UserRepository _userRepository;

  NameBloc(this._userRepository);

  @override
  NameState get initialState => InitiatedNameState();

  @override
  Stream<NameState> mapEventToState(NameEvent event) async* {
    if (event is SubmitNameEvent) {
      yield SubmittedNameState(event.value);

      if (event.value.isEmpty) {
        yield RequiredNameState(event.value, 'The name must be informed.');
      } else {
        final String name = await _userRepository.addName(event.value);
        yield ValidatedNameState(name);
      }
    }
  }
}
